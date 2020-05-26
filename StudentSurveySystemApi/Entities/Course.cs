using Server.Models.Survey;

namespace Server.Entities
{
    public class Course
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int SemesterId { get; set; }

        public int Year { get; set; }

        public SemesterPart SemesterPart { get; set; }

        public int? LeaderId { get; set; }

        public  User Leader { get; set; }
    }
}