using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using MediatR;


namespace LearningManagementSystem.Application.Features.Chapters.Commands.CreateQuiz
{
    public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, CreateQuizCommandResponse>
    {
        private readonly ICurrentUserService userService;
        private readonly IChapterRepository chapterRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly IChoiceRepository choiceRepository;

        public CreateQuizCommandHandler(ICurrentUserService userService, IChapterRepository chapterRepository, IQuestionRepository questionRepository, IChoiceRepository choiceRepository)
        {
            this.userService = userService;
            this.chapterRepository = chapterRepository;
            this.questionRepository = questionRepository;
            this.choiceRepository = choiceRepository;
        }

        public async Task<CreateQuizCommandResponse> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateQuizCommandValidator(userService, chapterRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateQuizCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            List<QuestionCreatedDto> questions = [];

            foreach (var q in request.QuestionList)
            {
                var question = Question.Create(q.Question, request.ChapterId);

                if (!question.IsSuccess)
                {
                    return new CreateQuizCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = [question.Error]
                    };
                }

                var result = await questionRepository.AddAsync(question.Value);

                if (!result.IsSuccess)
                {
                    return new CreateQuizCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = [result.Error]
                    };
                }

                QuestionCreatedDto questionCreatedDto = new QuestionCreatedDto
                {
                    QuestionId = question.Value.QuestionId
                };

                foreach (var c in q.Choices)
                {
                    var choice = Domain.Entities.Courses.Choice.Create(c.Choice, question.Value.QuestionId, c.IsCorrect);
                    
                    if (!choice.IsSuccess)
                    {
                        return new CreateQuizCommandResponse
                        {
                            Success = false,
                            ValidationsErrors = [choice.Error]
                        };
                    }

                    var result2 = await choiceRepository.AddAsync(choice.Value);

                    if (!result2.IsSuccess)
                    {
                        return new CreateQuizCommandResponse
                        {
                            Success = false,
                            ValidationsErrors = [result2.Error]
                        };
                    }

                    ChoiceCreatedDto choiceCreatedDto = new ChoiceCreatedDto()
                    {
                        ChoiceId = choice.Value.ChoiceId,
                        Choice = c
                    };

                    questionCreatedDto.Choices.Add(choiceCreatedDto);
                }

                questions.Add(questionCreatedDto);
            }

            return new CreateQuizCommandResponse
            {
                Success = true,
                QuizDto = new QuizDto { Questions = questions }
            };
        }
    }
}
