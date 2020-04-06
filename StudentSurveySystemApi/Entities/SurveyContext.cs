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
            
            modelBuilder.Entity<Course>()
                .HasOne(x => x.Leader)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Survey> Surveys { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<SurveyResponse> SurveyResponses { get; set; }

        public DbSet<Answer> Answer { get; set; }
    }
}