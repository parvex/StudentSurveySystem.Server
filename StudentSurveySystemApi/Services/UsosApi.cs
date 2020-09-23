using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;
using Server.Entities;
using Server.Models.Auth;

namespace Server.Services
{
    public interface IUsosApi
    {
        Task<UsosAuthDto> GetUsosAuthData(bool web = false);
        UsosUser GetUsosUserData(string token, string tokenSecret);
        (string, string) GetAccessTokenData(UsosAuthDto usosAuth);
        List<Semester> GetUserCourses(string accessToken, string tokenSecret, User user);
    }

    public class UsosApi : IUsosApi
    {
        public IRestClient Client;
        private readonly IConfiguration _configuration;
        private readonly string ConsumerKey = "B9anvbLDgvW8D6p4YByX";
        private readonly string ConsumerSecret = "vdr3Ew8zurEUA7VcJejGXvSbrfRhbnAHzkStUfMF";
        public UsosApi(IConfiguration configuration)
        {
            _configuration = configuration;
            Client = new RestClient("https://apps.usos.pw.edu.pl/services/");
            Client.UseNewtonsoftJson();
            Client.Authenticator = OAuth1Authenticator.ForRequestToken(ConsumerKey, ConsumerSecret, _configuration["MobileAuthRedirectUrl"]);
        }

        public async Task<UsosAuthDto> GetUsosAuthData(bool web = false)
        {
            if(web)
                Client.Authenticator = OAuth1Authenticator.ForRequestToken(ConsumerKey, ConsumerSecret, _configuration["WebAuthRedirectUrl"]);

            var request = new RestRequest("oauth/request_token");
            var response = await Client.ExecuteAsync(request, Method.GET);
            var requestTokenResponseParameters = HttpUtility.ParseQueryString(response.Content);

            var requestToken = requestTokenResponseParameters["oauth_token"];
            var requestSecret = requestTokenResponseParameters["oauth_token_secret"];
            var authRequest = new RestRequest("oauth/authorize?oauth_token=" + requestToken);
            
            return new UsosAuthDto { RequestToken = requestToken, 
                    TokenSecret = requestSecret, UsosAuthUrl = Client.BuildUri(authRequest).ToString() };
        }

        public UsosUser GetUsosUserData(string accessToken, string tokenSecret)
        {
            Client.Authenticator = OAuth1Authenticator
                .ForProtectedResource(ConsumerKey, ConsumerSecret, accessToken, tokenSecret);

            var userDataRequest = new RestRequest("users/user", Method.GET);
            userDataRequest.AddParameter("fields", "id|first_name|last_name|student_status|staff_status");

            var userDataResponse = Client.Execute<UsosUser>(userDataRequest);
            return userDataResponse.IsSuccessful ? userDataResponse.Data : null;
        }

        public (string, string) GetAccessTokenData(UsosAuthDto usosAuth)
        {
            Client.Authenticator = OAuth1Authenticator
                .ForAccessToken(ConsumerKey, ConsumerSecret, usosAuth.RequestToken, usosAuth.TokenSecret, usosAuth.OAuthVerifier);
            var request = new RestRequest("oauth/access_token");
            var result = Client.Execute(request);

            var requestActionTokenResponseParameters = HttpUtility.ParseQueryString(result.Content);
            var accessToken = requestActionTokenResponseParameters["oauth_token"];
            var accessSecret = requestActionTokenResponseParameters["oauth_token_secret"];
            return (accessToken, accessSecret);
        }

        public List<Semester> GetUserCourses(string accessToken, string tokenSecret, User user)
        {
            Client.Authenticator = OAuth1Authenticator
                .ForProtectedResource(ConsumerKey, ConsumerSecret, accessToken, tokenSecret);

            var userDataRequest = new RestRequest("courses/user", Method.GET);

            var userDataResponse = Client.Execute(userDataRequest);
            if(!userDataResponse.IsSuccessful)
                throw new Exception("Fetching usos data error");
            JObject json = JObject.Parse(userDataResponse.Content);
            var semesters = new List<Semester>();

            foreach (var semJson in json["course_editions"].Children())
            {
                var semester = new Semester() { Name = semJson.ToObject<JProperty>().Name };
                semester.Courses = new List<Course>();
                foreach (var courseJson in semJson.First.Children())
                {
                    var course = new Course() {Name = courseJson.Value<string>("course_id")};
                    semester.Courses.Add(course);
                }
                semesters.Add(semester);
            }

            if (user.UserRole != UserRole.Lecturer) 
                return semesters;

            var lecturerSemesters = GetUserCoursesAsLecturer(accessToken, tokenSecret, user);
            semesters = MergeSemesterData(semesters, lecturerSemesters, user.Id.Value);

            return semesters;
        }

        public List<Semester> GetUserCoursesAsLecturer(string accessToken, string tokenSecret, User user)
        {
            Client.Authenticator = OAuth1Authenticator
                .ForProtectedResource(ConsumerKey, ConsumerSecret, accessToken, tokenSecret);

            var groupsRequest = new RestRequest("groups/lecturer", Method.GET);
            groupsRequest.AddParameter("fields", "course_id|course_name|lecturers");

            var groupsResponse = Client.Execute(groupsRequest);
            JObject json = JObject.Parse(groupsResponse.Content);
            var semesters = new List<Semester>();

            foreach (var semJson in json["groups"].Children())
            {
                var semester = new Semester() { Name = semJson.ToObject<JProperty>().Name };
                semester.Courses = new List<Course>();
                foreach (var courseJson in semJson.First.Children())
                {
                    var course = new Course() { Name = courseJson.Value<string>("course_id") };
                    course.CourseLecturers = new List<CourseLecturer>() { new CourseLecturer() { LecturerId = user.Id.Value } };

                    semester.Courses.Add(course);
                }
                semesters.Add(semester);
            }
            return semesters;
        }

        private List<Semester> MergeSemesterData(List<Semester> semestersAsStudent, List<Semester> semestersAsLecturer, int userId)
        {
            var mergedSemesters = new List<Semester>();
            mergedSemesters.AddRange(semestersAsStudent);
            mergedSemesters.AddRange(semestersAsLecturer);

            mergedSemesters = mergedSemesters.GroupBy(x => x.Name).Select(x => new Semester() { Name = x.Key }).ToList();

            foreach (var semester in mergedSemesters)
            {
                semester.Courses = new List<Course>();
                if (semestersAsStudent.Any(x => x.Name == semester.Name))
                {
                    semester.Courses.AddRange(semestersAsStudent.First(x => x.Name == semester.Name).Courses);
                }
                if (semestersAsLecturer.Any(x => x.Name == semester.Name))
                {
                    semester.Courses.AddRange(semestersAsLecturer.First(x => x.Name == semester.Name).Courses);
                }

                semester.Courses = semester.Courses.GroupBy(x => x.Name).Select(x => new Course() { Name = x.Key }).ToList();
            }

            foreach (var semester in semestersAsLecturer)
            {
                foreach (var course in semester.Courses)
                {
                    mergedSemesters.First(x => x.Name == semester.Name)
                        .Courses.First(x => x.Name == course.Name)
                        .CourseLecturers = new List<CourseLecturer>() { new CourseLecturer() { LecturerId = userId } };
                }
            }

            return mergedSemesters;
        }
    }
}