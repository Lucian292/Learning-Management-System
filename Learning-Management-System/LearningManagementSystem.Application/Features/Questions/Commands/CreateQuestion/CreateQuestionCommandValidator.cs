using FluentValidation;
using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;

namespace LearningManagementSystem.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
    {
        private readonly ICurrentUserService userService;
        private readonly IChapterRepository chapterRepository;
        public CreateQuestionCommandValidator(ICurrentUserService userService, IChapterRepository chapterRepository)
        {
            this.userService = userService;
            this.chapterRepository = chapterRepository;

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Question text is required")
                .NotNull()
                .MaximumLength(300).WithMessage("{PropertyName} must not exceed 300 characters");
            
            RuleFor(p => p.ChapterId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.")
                .MustAsync(async (chapterId, cancellationToken) =>
                {
                    if (userService.IsUserAdmin())
                        return true;

                    var userId = Guid.Parse(userService.UserId);

                    var chapter = await chapterRepository.FindByIdAsync(chapterId);

                    if (!chapter.IsSuccess) 
                        return false;

                    var isOwner = chapter.Value.Course.ProfessorId == userId;

                    return isOwner;
                }).WithMessage("User doesn't own the course");
        }
    }
}
