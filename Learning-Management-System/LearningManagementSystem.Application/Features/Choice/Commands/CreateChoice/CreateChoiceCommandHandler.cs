
using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;


namespace LearningManagementSystem.Application.Features.Choice.Commands.CreateChoice
{
    public class CreateChoiceCommandHandler : IRequestHandler<CreateChoiceCommand, CreateChoiceCommandResponse>
    {
        private readonly IChoiceRepository choiceRepository;

        public CreateChoiceCommandHandler(IChoiceRepository choiceRepository)
        {
            this.choiceRepository = choiceRepository;
        }

        public async Task<CreateChoiceCommandResponse> Handle(CreateChoiceCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateChoiceCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            
            if (!validationResult.IsValid)
            {
                return new CreateChoiceCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var choice = Domain.Entities.Courses.Choice.Create(request.Content, request.QuestionId, request.IsCorrect);

            if (!choice.IsSuccess)
            {
                return new CreateChoiceCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { choice.Error }
                };
            }

            await choiceRepository.AddAsync(choice.Value);

            return new CreateChoiceCommandResponse
            {
                Success = true,
                Choice = new CreateChoiceDto
                {
                    ChoiceId = choice.Value.ChoiceId,
                    Content = choice.Value.Content,
                    IsCorrect = choice.Value.IsCorrect,
                    QuestionId = choice.Value.QuestionId
                }
            };
        }
    }

}
