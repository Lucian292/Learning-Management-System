using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, CreateQuestionCommandResponse>
    {
        private readonly IQuestionRepository repository;

        public CreateQuestionCommandHandler(IQuestionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateQuestionCommandResponse> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateQuestionCommandValidator();
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
