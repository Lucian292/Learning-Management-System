using FluentValidation;
using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.CreateQuiz
{
    public class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
    {
        private readonly ICurrentUserService userService;
        private readonly IChapterRepository chapterRepository;

        public CreateQuizCommandValidator(ICurrentUserService userService, IChapterRepository chapterRepository)
        {
            this.userService = userService;
            this.chapterRepository = chapterRepository;

            RuleFor(p => p.ChapterId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.")
                .MustAsync(async (chapterId, cancellationToken) =>
                {
                    var userId = Guid.Parse(userService.UserId);

                    var chapter = await chapterRepository.FindByIdAsync(chapterId);

                    if (!chapter.IsSuccess)
                        return false;

                    if (chapter.Value.Quizz.Count > 0)
                        return false;

                    if (userService.IsUserAdmin())
                        return true;

                    var isOwner = chapter.Value.Course.ProfessorId == userId;

                    return isOwner;
                }).WithMessage("Can't add a quiz to this chapter");

            RuleFor(p => p.QuestionList)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Must((questionList) =>
                {
                    if (questionList.Count != 10)
                        return false;

                    foreach (var question in questionList)
                    {
                        if (string.IsNullOrEmpty(question.Question) || question.Choices.Count != 3)
                            return false;

                        int goodAnswersCount = 0;
                        foreach (var choice in question.Choices)
                        {
                            if (string.IsNullOrEmpty(choice.Choice))
                                return false;
                            if (choice.IsCorrect)
                                goodAnswersCount++;
                        }

                        if (goodAnswersCount != 1)
                            return false;
                    }
                    
                    return true;
                }).WithMessage("Invalid format");
        }
    }
}
