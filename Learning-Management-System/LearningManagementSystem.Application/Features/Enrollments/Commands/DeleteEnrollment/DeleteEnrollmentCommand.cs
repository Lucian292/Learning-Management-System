using MediatR;

namespace LearningManagementSystem.Application.Features.Enrollments.Commands.DeleteEnrollment
{
    public class DeleteEnrollmentCommand : IRequest<DeleteEnrollmentCommandResponse>
    {
        public Guid EnrollmentId { get; set; }
    }
}
