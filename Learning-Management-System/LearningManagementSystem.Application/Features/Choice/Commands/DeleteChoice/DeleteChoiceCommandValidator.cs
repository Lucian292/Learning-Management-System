using FluentValidation;

namespace LearningManagementSystem.Application.Features.Choice.Commands.DeleteChoice
{
    public class DeleteChoiceCommandValidator : AbstractValidator<DeleteChoiceCommand>
    {
        public DeleteChoiceCommandValidator()
        {
            RuleFor(p => p.ChoiceId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
