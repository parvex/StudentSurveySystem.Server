using Microsoft.EntityFrameworkCore;

namespace Server.Entities
{
    public class SurveyContext : DbContext
    {
        public SurveyContext(DbContextOptions<SurveyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Survey>()
                .HasOne(x => x.Creator)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<CourseLecturer>()
                .HasOne(cl => cl.Lecturer)
                .WithMany(l => l.CourseLecturers)
                .HasForeignKey(cl => cl.LecturerId);
            modelBuilder.Entity<CourseLecturer>()
                .HasOne(cl => cl.Course)
                .WithMany(l => l.CourseLecturers)
                .HasForeignKey(cl => cl.CourseId);

            modelBuilder.Entity<CourseParticipant>()
                .HasOne(cp => cp.Participant)
                .WithMany(p => p.CourseParticipants)
                .HasForeignKey(cl => cl.ParticipantId);
            modelBuilder.Entity<CourseParticipant>()
                .HasOne(cp => cp.Course)
                .WithMany(p => p.CourseParticipants)
                .HasForeignKey(cl => cl.CourseId);

            modelBuilder.Entity<Semester>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<Course>()
                .HasIndex(x => new { x.Name, x.SemesterId })
                .IsUnique();

            modelBuilder.Entity<CourseParticipant>()
                .HasIndex(x => new {x.CourseId, x.ParticipantId})
                .IsUnique();
            modelBuilder.Entity<CourseLecturer>()
                .HasIndex(x => new { x.CourseId, x.LecturerId })
                .IsUnique();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Survey> Surveys { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<SurveyResponse> SurveyResponses { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<CourseLecturer> CourseLecturers { get; set; }

        public DbSet<CourseParticipant> CourseParticipants { get; set; }
    }
}