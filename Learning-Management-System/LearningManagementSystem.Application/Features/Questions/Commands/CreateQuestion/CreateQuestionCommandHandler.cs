using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, CreateQuestionCommandResponse>
    {
        private readonly IQuestionRepository repository;
        private readonly IChapterRepository chapterRepository;
        private readonly ICurrentUserService userService;

        public CreateQuestionCommandHandler(IQuestionRepository repository, IChapterRepository chapterRepository, ICurrentUserService userService, ICourseRepository courseRepository)
        {
            this.repository = repository;
            this.chapterRepository = chapterRepository;
            this.userService = userService;
        }

        public async Task<CreateQuestionCommandResponse> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateQuestionCommandValidator(userService, chapterRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            
            if (!validationResult.IsValid)
            {
                return new CreateQuestionCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var question = Question.Create(request.Text, request.ChapterId);
            if (!question.IsSuccess)
            {
                return new CreateQuestionCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { question.Error }
                };
            }

            await repository.AddAsync(question.Value);

            return new CreateQuestionCommandResponse
            {
                Success = true,
                Question = new CreateQuestionDto
                {
                    QuestionId = question.Value.QuestionId,
                    Text = question.Value.Text,
                    ChapterId = question.Value.ChapterId
                }
            };
        }
    }

}
