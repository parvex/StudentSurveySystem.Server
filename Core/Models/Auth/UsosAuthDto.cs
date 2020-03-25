using System;

namespace StudentSurveySystem.Core.Models.Auth
{
    public class UsosAuthDto
    {
        public string UsosAuthUrl { get; set; }

        public string RequestToken { get; set; }

        public string TokenSecret { get; set; }

        public string OAuthVerifier { get; set; }
    }
}