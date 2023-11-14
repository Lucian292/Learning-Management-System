using FluentValidation;

namespace LearningManagementSystem.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Question text is required")
                .NotNull()
                .MaximumLength(300).WithMessage("{PropertyName} must not exceed 300 characters");
        }
    }
}
