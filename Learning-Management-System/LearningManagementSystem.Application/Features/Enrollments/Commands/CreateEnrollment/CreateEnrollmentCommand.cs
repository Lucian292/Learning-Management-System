using MediatR;

namespace LearningManagementSystem.Application.Features.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommand : IRequest<CreateEnrollmentCommandResponse>
    {
        public Guid CourseId { get; set; } = default!;
        public decimal Progress { get; set; } = default!;
    }
}
