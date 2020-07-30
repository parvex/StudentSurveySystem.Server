using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Server.Services
{
    public interface IPushNotificationService
    {
        Task Send(string title, string message, string topic = "global");
    }

    public class PushNotificationService : IPushNotificationService
    {
        private readonly IConfiguration _configuration;
        private readonly IRestClient Client;

        public PushNotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
            Client = new RestClient("https://fcm.googleapis.com/fcm/");
            Client.UseNewtonsoftJson();
            var apiKey = _configuration["FcmApiKey"];
            Client.AddDefaultHeader("Authorization", "key=" + apiKey);
        }

        public async Task Send(string title, string message, string topic = "global")
        {
            var request = new RestRequest("send");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new
            {
                to = "/topics/" + topic,
                priority= "high",
                data = new
                {
                    title = title, 
                    message = message
                }
            });
            var response = await Client.ExecuteAsync(request, Method.POST);
            return;
        }
    }
}