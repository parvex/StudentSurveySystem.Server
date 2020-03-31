using System;
using System.Threading.Tasks;
using System.Web;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth;
using RestSharp.Serializers.NewtonsoftJson;
using StudentSurveySystem.Core.Models.Auth;
using StudentSurveySystemApi.Helpers;

namespace StudentSurveySystemApi.Services
{
    public class UsosApi
    {
        private readonly IRestClient Client;
        private readonly string ConsumerKey = "B9anvbLDgvW8D6p4YByX";
        private readonly string ConsumerSecret = "vdr3Ew8zurEUA7VcJejGXvSbrfRhbnAHzkStUfMF";
        public UsosApi()
        {
            Client = new RestClient("https://apps.usos.pw.edu.pl/services/");
            Client.UseNewtonsoftJson();
            //TODO: callback url, maybe it can lead to mobile client again
            Client.Authenticator = OAuth1Authenticator.ForRequestToken(ConsumerKey, ConsumerSecret, "https://studentsurveysystemapi20200128125529.azurewebsites.net");
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

        public UsosUser GetUsosUserData(UsosAuthDto usosAuth)
        {
            var (token, tokenSecret) = GetAccessTokenData(usosAuth);
            Client.Authenticator = OAuth1Authenticator
                .ForProtectedResource(ConsumerKey, ConsumerSecret, token, tokenSecret);

            var userDataRequest = new RestRequest("users/user", Method.GET);
            userDataRequest.AddParameter("fields", "id|first_name|last_name|student_status|staff_status");

            var userDataResponse = Client.Execute<UsosUser>(userDataRequest);
            return userDataResponse.IsSuccessful ? userDataResponse.Data : null;
        }

        private (string, string) GetAccessTokenData(UsosAuthDto usosAuth)
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

        private T Execute<T>(RestRequest request)
        {
            var response = Client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var exception = new Exception(message, response.ErrorException);
                throw exception;
            }
            return response.Data;
        }
    }
}