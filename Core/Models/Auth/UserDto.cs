﻿namespace Core.Models.Auth
{
    public class UserDto
    {
        public int? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public UserRole UserRole { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}