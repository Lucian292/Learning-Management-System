using MediatR;

namespace LearningManagementSystem.Application.Features.Tags.Queries.GetById
{
    public class GetByIdTagQuery : IRequest<TagDto>
    {
        public Guid TagId { get; set; }
    }
}
