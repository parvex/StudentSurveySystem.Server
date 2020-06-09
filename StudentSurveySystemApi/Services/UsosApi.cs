using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
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
        Task<UsosAuthDto> GetUsosAuthData();
        UsosUser GetUsosUserData(string token, string tokenSecret);
        (string, string) GetAccessTokenData(UsosAuthDto usosAuth);
    }

    public class UsosApi : IUsosApi
    {
        private readonly IConfiguration _configuration;
        private readonly IRestClient Client;
        private readonly string ConsumerKey = "B9anvbLDgvW8D6p4YByX";
        private readonly string ConsumerSecret = "vdr3Ew8zurEUA7VcJejGXvSbrfRhbnAHzkStUfMF";
        public UsosApi(IConfiguration configuration)
        {
            _configuration = configuration;
            Client = new RestClient("https://apps.usos.pw.edu.pl/services/");
            Client.UseNewtonsoftJson();
            Client.Authenticator = OAuth1Authenticator.ForRequestToken(ConsumerKey, ConsumerSecret, _configuration["AuthRedirectUrl"]);
        }

        public async Task<UsosAuthDto> GetUsosAuthData()
        {
            var baseUrl = new Uri("https://apps.usos.pw.edu.pl/services/");
            var request = new RestRequest("oauth/request_token");
            //request.AddParameter("scopes", "");
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
            var x = GetUserCourses(accessToken, tokenSecret);


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

        public UsosCourses GetUserCourses(string accessToken, string tokenSecret)
        {
            Client.Authenticator = OAuth1Authenticator
                .ForProtectedResource(ConsumerKey, ConsumerSecret, accessToken, tokenSecret);

            var userDataRequest = new RestRequest("courses/user", Method.GET);
            //userDataRequest.AddParameter("fields", "id|first_name|last_name|student_status|staff_status");



            var userDataResponse = Client.Execute(userDataRequest);

            dynamic json = JObject.Parse(userDataResponse.Content);

            var x = json.course_editions;


            return /*userDataResponse.IsSuccessful ? userDataResponse.Data :*/ null;
        }


    }
}