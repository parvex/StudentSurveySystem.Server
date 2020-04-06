namespace Core.Models.Auth
{
    public class CurrentUserDto
    {
        public int? Id { get; set; }

        public int? UsosId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public UserRole UserRole { get; set; }

        public string Token { get; set; }
    }
}