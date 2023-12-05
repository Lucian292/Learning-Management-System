namespace LearningManagementSystem.Application.Features.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentDto
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public decimal? Progress { get; set; }
        
    }
}
