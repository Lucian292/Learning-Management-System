using LearningManagementSystem.Domain.Entities;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

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
        public DbSet<User> Users { get; set; }

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
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
