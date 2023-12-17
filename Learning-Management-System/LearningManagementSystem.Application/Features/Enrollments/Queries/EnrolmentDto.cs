using LearningManagementSystem.Application.Features.Courses.Queries;

namespace LearningManagementSystem.Application.Features.Enrollments.Queries
{
    public class EnrollmentDto
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public decimal? Progress { get; set; } = default!;
        public CourseDto? Course { get; set; }
    }
}