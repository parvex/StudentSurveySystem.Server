namespace Server.Entities
{
    public class CourseParticipant
    {
        public int Id { get; set; }

        public int ParticipantId { get; set; }
        public User Participant { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}