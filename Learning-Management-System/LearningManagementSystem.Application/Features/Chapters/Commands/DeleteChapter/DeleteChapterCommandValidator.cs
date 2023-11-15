using FluentValidation;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.DeleteChapter
{
    public class DeleteChapterCommandValidator : AbstractValidator<DeleteChapterCommand>
    {
        public DeleteChapterCommandValidator()
        {
            RuleFor(p => p.ChapterId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
