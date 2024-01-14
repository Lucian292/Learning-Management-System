using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Enrollments.Commands.DeleteEnrollment
{
    public class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, DeleteEnrollmentCommandResponse>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public DeleteEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<DeleteEnrollmentCommandResponse> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteEnrollmentCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new DeleteEnrollmentCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var enrollment = await _enrollmentRepository.FindByIdAsync(request.EnrollmentId);

            if (!enrollment.IsSuccess)
            {
                return new DeleteEnrollmentCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { enrollment.Error }
                };
            }

            await _enrollmentRepository.DeleteAsync(request.EnrollmentId);

            return new DeleteEnrollmentCommandResponse
            {
                Success = true
            };
        }
    }
}
