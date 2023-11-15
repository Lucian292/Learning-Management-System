using FluentValidation;

namespace LearningManagementSystem.Application.Features.Enrollments.Commands.DeleteEnrollment
{
    public class DeleteEnrollmentCommandValidator : AbstractValidator<DeleteEnrollmentCommand>
    {
        public DeleteEnrollmentCommandValidator()
        {
            RuleFor(p => p.EnrollmentId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
