using System.ComponentModel.DataAnnotations;

namespace StudentSurveySystem.Core.Models
{
    public class AuthenticateDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}