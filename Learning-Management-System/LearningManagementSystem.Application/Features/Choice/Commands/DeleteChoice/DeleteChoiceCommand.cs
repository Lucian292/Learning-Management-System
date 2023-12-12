using MediatR;

namespace LearningManagementSystem.Application.Features.Choice.Commands.DeleteChoice
{
    public class DeleteChoiceCommand : IRequest<DeleteChoiceCommandResponse>
    {
        public Guid ChoiceId { get; set; }
    }
}
