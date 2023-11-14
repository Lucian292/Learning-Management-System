using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, CreateQuestionCommandResponse>
    {
        private readonly IQuestionRepository repository;
        private readonly ISender mediator;

        public CreateQuestionCommandHandler(IQuestionRepository repository, ISender mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
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

            var question = Question.Create(request.Text);
            if (!question.IsSuccess)
            {
                return new CreateQuestionCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { question.Error }
                };
            }

/*            // Adaugă variantele de răspuns la întrebare
            if (request.Choices != null && request.Choices.Any())
            {
                foreach (var createChoiceCommand in request.Choices)
                {
                    var choiceId = await mediator.Send(createChoiceCommand);
                    var choiceResult = await repository.GetAllAsync();
                    if (choiceResult.IsSuccess)
                    {
                        question.Value.AttachChoice((Domain.Entities.Courses.Choice)choiceResult.Value);
                    }
                    else
                    {
                        // Tratează eroare la găsirea opțiunii
                        throw new ApplicationException(choiceResult.Error);
                    }
                }
            }*/

            await repository.AddAsync(question.Value);

            return new CreateQuestionCommandResponse
            {
                Success = true,
                Question = new CreateQuestionDto
                {
                    QuestionId = question.Value.QuestionId,
                    Text = question.Value.Text
                }
            };
        }
    }

}
