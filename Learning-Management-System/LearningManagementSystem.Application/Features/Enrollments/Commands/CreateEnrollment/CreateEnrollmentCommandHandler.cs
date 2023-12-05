using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommandHandler : IRequestHandler<CreateEnrollmentCommand, CreateEnrollmentCommandResponse>
    {
        private readonly IEnrollmentRepository repository;
        private readonly ICurrentUserService userService;

        public CreateEnrollmentCommandHandler(IEnrollmentRepository repository, ICurrentUserService userService)
        {
            this.repository = repository;
            this.userService = userService;
        }

        public async Task<CreateEnrollmentCommandResponse> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEnrollmentCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateEnrollmentCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var userId = Guid.Parse(userService.UserId);

            var enrollment = Enrollment.Create(userId, request.CourseId);
            if (!enrollment.IsSuccess)
            {
                return new CreateEnrollmentCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { enrollment.Error }
                };
            }

            await repository.AddAsync(enrollment.Value);

            return new CreateEnrollmentCommandResponse
            {
                Success = true,
                Enrollment = new CreateEnrollmentDto
                {
                    CourseId = enrollment.Value.CourseId,
                    UserId = enrollment.Value.UserId,
                    Progress = enrollment.Value.Progress
                }
            };
        }

    }
}
