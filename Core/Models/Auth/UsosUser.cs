using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StudentSurveySystem.Core.Models.Auth
{
    public class UsosUser
    {
        [JsonProperty("id")]
        public int UsosId { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}