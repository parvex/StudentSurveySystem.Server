using System.Collections.Generic;

namespace Server.Models.Survey
{
    public class SemesterDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<CourseDto> Courses { get; set; }
    }
}