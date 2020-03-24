using StudentSurveySystem.Core.Models;

namespace StudentSurveySystemApi.Entities
{
    public class User
    {
        public int? Id { get; set; }

        public int? UsosId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public UserRole UserRole { get; set; }
    }
}