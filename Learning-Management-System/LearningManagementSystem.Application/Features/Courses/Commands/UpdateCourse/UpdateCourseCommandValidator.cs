using FluentValidation;

namespace LearningManagementSystem.Application.Features.Courses.Commands.UpdateCourseCommand
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(x => x.UpdateCourseDto.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
            RuleFor(x => x.UpdateCourseDto.Description)
                .MinimumLength(300).WithMessage("{PropertyName} must be at least 300 characters.");
        }
    }
}
