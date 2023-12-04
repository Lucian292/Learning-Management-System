using FluentValidation;

namespace LearningManagementSystem.Application.Features.Choice.Commands.CreateChoice
{
    public class CreateChoiceCommandValidator : AbstractValidator<CreateChoiceCommand>
    {
        public CreateChoiceCommandValidator()
        {
            RuleFor(p => p.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(300).WithMessage("{PropertyName} must not exceed 300 characters.");
            RuleFor(p => p.QuestionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(System.Guid.Empty).WithMessage("{PropertyName} must not be empty.");
            RuleFor(p => p.IsCorrect)
                .InclusiveBetween(false, true)
                .WithMessage("{PropertyName} must be true or false.");

        }
    }
}
