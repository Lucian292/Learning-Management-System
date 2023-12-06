using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities;
using LearningManagementSystem.Domain.Entities.Courses;
using Microsoft.EntityFrameworkCore;


namespace LearningManagementSystem.Infrastructure.Data
{
    public class LearningManagementSystemDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly ICurrentUserService currentUserService;

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
                .HasKey(ct => new { ct.CourseId, ct.TagId });

            modelBuilder.Entity<CourseTag>()
                .HasOne(ct => ct.Course)
                .WithMany(c => c.CourseTags)
                .HasForeignKey(ct => ct.CourseId);

            modelBuilder.Entity<CourseTag>()
                .HasOne(ct => ct.Tag)
                .WithMany(t => t.TagsCourses)
                .HasForeignKey(ct => ct.TagId);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = currentUserService.GetCurrentClaimsPrincipal()?.Claims.FirstOrDefault(c => c.Type == "name")?.Value!;
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedBy = currentUserService.GetCurrentClaimsPrincipal()?.Claims.FirstOrDefault(c => c.Type == "name")?.Value!;
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        public LearningManagementSystemDbContext(
            DbContextOptions<LearningManagementSystemDbContext> options, ICurrentUserService currentUserService) :
            base(options)
        {
            this.currentUserService = currentUserService;
        }
    }
}
