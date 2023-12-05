using FluentValidation;

namespace LearningManagementSystem.Application.Features.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommandValidator : AbstractValidator<CreateEnrollmentCommand>
    {
        public CreateEnrollmentCommandValidator()
        {
            RuleFor(p => p.CourseId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
