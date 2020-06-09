using System.Collections.Generic;

namespace Server.Entities
{
    public class Semester
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public List<Course> Courses { get; set; }
    }
}