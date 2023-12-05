using MediatR;

namespace LearningManagementSystem.Application.Features.Tags.Commands.DeleteTag
{
    public class DeleteTagCommand : IRequest<DeleteTagCommandResponse>
    {
        public Guid TagId { get; set; }
    }
}
