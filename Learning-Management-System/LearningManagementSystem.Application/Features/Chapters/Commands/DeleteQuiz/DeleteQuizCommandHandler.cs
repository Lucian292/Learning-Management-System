using MediatR;
using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.DeleteQuiz
{
    public class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand, DeleteQuizCommandResponse>
    {
        private readonly ICurrentUserService userService;
        private readonly IChapterRepository chapterRepository;
        private readonly IQuestionRepository questionRepository;

        public DeleteQuizCommandHandler(ICurrentUserService userService, IChapterRepository chapterRepository, IQuestionRepository questionRepository)
        {
            this.userService = userService;
            this.chapterRepository = chapterRepository;
            this.questionRepository = questionRepository;
        }

        public async Task<DeleteQuizCommandResponse> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteQuizCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new DeleteQuizCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var chapter = await chapterRepository.FindByIdAsync(request.ChapterId);

            if (!chapter.IsSuccess)
            {
                return new DeleteQuizCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { chapter.Error }
                };
            }

            var userId = Guid.Parse(userService.UserId);

            if (chapter.Value.Course.ProfessorId != userId && !userService.IsUserAdmin())
            {
                return new DeleteQuizCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "User doesn't own the course" }
                };
            }

            List<Guid> questionsIds = new List<Guid>();
            foreach (var question in chapter.Value.Quizz)
            {
                questionsIds.Add(question.QuestionId);
            }

            foreach(var questionId in questionsIds)
            {
                await questionRepository.DeleteAsync(questionId);
            }

            return new DeleteQuizCommandResponse
            {
                Success = true
            };
        }
    }
}
