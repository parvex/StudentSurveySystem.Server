﻿using System.Collections.Generic;
using Server.Models.Auth;

namespace Server.Entities
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

        public List<CourseParticipant> CourseParticipants { get; set; }

        public List<CourseLecturer> CourseLecturers { get; set; }
    }
}