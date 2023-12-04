using LearningManagementSystem.Domain.Entities;
using LearningManagementSystem.Domain.Entities.Courses;
using Microsoft.EntityFrameworkCore;


namespace LearningManagementSystem.Infrastructure.Data
{
    public class LearningManagementSystemDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<QuestionResult> QuestionResults { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Enrollment>()
                .HasMany(e => e.QuizzResults)
                .WithOne()
                .HasForeignKey("EnrollmentId");

            modelBuilder.Entity<EnrollmentQuestionResult>()
                .HasKey(eqr => new { eqr.EnrollmentId, eqr.ChapterId, eqr.QuestionResultId });

            modelBuilder.Entity<EnrollmentQuestionResult>()
                .HasOne(eqr => eqr.Enrollment)
                .WithMany(e => e.QuizzResults)
                .HasForeignKey(eqr => eqr.EnrollmentId);

            modelBuilder.Entity<EnrollmentQuestionResult>()
                .HasOne(eqr => eqr.Chapter)
                .WithMany()
                .HasForeignKey(eqr => eqr.ChapterId);

            modelBuilder.Entity<EnrollmentQuestionResult>()
                .HasOne(eqr => eqr.QuestionResult)
                .WithMany()
                .HasForeignKey(eqr => eqr.QuestionResultId);

            modelBuilder.Entity<CourseTag>()
                .HasKey(ct => new { ct.CourseId, ct.TagId });

            modelBuilder.Entity<CourseTag>()
                .HasOne(c => c.Course)
                .WithMany(c => c.CourseTags)
                .HasForeignKey(c => c.CourseId);

            modelBuilder.Entity<CourseTag>()
                .HasOne(c => c.Tag)
                .WithMany(c => c.CourseTags)
                .HasForeignKey(c => c.TagId);
        }


        public LearningManagementSystemDbContext(
            DbContextOptions<LearningManagementSystemDbContext> options) :
            base(options)
        {

        }
    }
}
