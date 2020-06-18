namespace Server.Models.Survey
{
    public class CourseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SemesterId { get; set; }

        public string SemesterName { get; set; }

        public int LeaderId { get; set; }
    }
}