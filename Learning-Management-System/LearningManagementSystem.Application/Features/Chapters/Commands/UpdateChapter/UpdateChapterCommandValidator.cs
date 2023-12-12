using FluentValidation;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.UpdateChapter
{
    public class UpdateChapterCommandValidator : AbstractValidator<UpdateChapterCommand>
    {
        public UpdateChapterCommandValidator()
        {
            RuleFor(p => p.Chapter.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
