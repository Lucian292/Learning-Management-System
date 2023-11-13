using MediatR;


namespace LearningManagementSystem.Application.Features.Users.Queries.GetById
{
    public record GetByIdUserQuery(Guid Id) : IRequest<UserDto>;
}
