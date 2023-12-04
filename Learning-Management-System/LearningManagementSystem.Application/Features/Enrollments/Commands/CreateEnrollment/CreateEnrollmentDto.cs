namespace LearningManagementSystem.Application.Features.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentDto
    {
        public string UserName { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
        public decimal? Progress { get; set; }
        
    }
}
