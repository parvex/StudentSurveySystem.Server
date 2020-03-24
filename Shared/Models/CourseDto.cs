namespace StudentSurveySystem.Core.Models
{
    public class CourseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SemesterId { get; set; }

        public int LeaderId { get; set; }
    }
}