using LearningManagementSystem.Application.Features.Chapters.Commands.CreateChapter;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommandHandler : IRequestHandler<CreateEnrollmentCommand, CreateEnrollmentCommandResponse>
    {
        private readonly IEnrollmentRepository repository;

        public CreateEnrollmentCommandHandler(IEnrollmentRepository repository)
        {
            this.repository = repository;
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

            var enrollment = Enrollment.Create(request.CourseId, request.UserId);
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
