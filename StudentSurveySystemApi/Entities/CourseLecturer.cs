namespace Server.Entities
{
    public class CourseLecturer
    {
        public int Id { get; set; }

        public int LecturerId { get; set; }
        public User Lecturer { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}