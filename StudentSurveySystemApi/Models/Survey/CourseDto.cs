namespace Server.Models.Survey
{
    public class CourseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SemesterPart SemesterPart { get; set; }

        public int LeaderId { get; set; }
    }
}