

using MediatR;

namespace LearningManagementSystem.Application.Features.Choice.Commands.CreateChoice
{
    public class CreateChoiceCommand : IRequest<CreateChoiceCommandResponse>
    {
        public Guid QuestionId { get; set; }
        public string Content { get; set; } = default!;
        public bool IsCorrect { get; set; }
    }
}
