using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Server.Models.Survey;

namespace Server.Entities
{
    public class Course
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int SemesterId { get; set; }

        [ForeignKey(nameof(SemesterId))]
        public Semester Semester { get; set; }

        public List<CourseLecturer> CourseLecturers { get; set; }

        public List<CourseParticipant> CourseParticipants { get; set; }
    }
}