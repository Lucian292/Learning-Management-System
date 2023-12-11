using FluentValidation;
using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;

namespace LearningManagementSystem.Application.Features.Choice.Commands.CreateChoice
{
    public class CreateChoiceCommandValidator : AbstractValidator<CreateChoiceCommand>
    {
        private readonly ICurrentUserService userService;
        private readonly IQuestionRepository questionRepository;
        private readonly IChapterRepository chapterRepository;

        public CreateChoiceCommandValidator(ICurrentUserService userService, IChapterRepository chapterRepository, IQuestionRepository questionRepository)
        {
            this.userService = userService;
            this.questionRepository = questionRepository;
            this.chapterRepository = chapterRepository;

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
            RuleFor(p => p.QuestionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.")
                .MustAsync(async (questionId, cancellationToken) =>
                {
                    if (userService.IsUserAdmin())
                        return true;

                    var userId = Guid.Parse(userService.UserId);

                    var question = await questionRepository.FindByIdAsync(questionId);

                    var chapter = await chapterRepository.FindByIdAsync(question.Value.ChapterId);

                    if (!chapter.IsSuccess)
                        return false;

                    var isOwner = chapter.Value.Course.ProfessorId == userId;

                    return isOwner;
                }).WithMessage("User doesn't own the course");
        }
    }
}
