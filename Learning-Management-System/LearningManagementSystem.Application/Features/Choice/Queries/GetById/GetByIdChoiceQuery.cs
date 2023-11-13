using MediatR;

namespace LearningManagementSystem.Application.Features.Choice.Queries.GetById
{
    public record GetByIdChoiceQuery(Guid Id) : IRequest<ChoiceDto>;
}
