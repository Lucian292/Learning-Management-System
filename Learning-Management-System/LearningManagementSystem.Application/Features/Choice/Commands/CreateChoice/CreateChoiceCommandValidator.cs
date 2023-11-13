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
        }
    }
}
