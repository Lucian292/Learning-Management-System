namespace LearningManagementSystem.Application.Features.Enrollments.Queries
{
    public class EnrollmentDto
    {
        public string UserName { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
        public decimal? Progress { get; set; } = default!;
    }
}