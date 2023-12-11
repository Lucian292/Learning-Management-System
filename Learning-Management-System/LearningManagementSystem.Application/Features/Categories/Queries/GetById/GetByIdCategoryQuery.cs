using MediatR;

namespace LearningManagementSystem.Application.Features.Categories.Queries.GetById
{
    public record GetByIdCategoryQuery(Guid Id) : IRequest<GetSingleCategoryDto>; 
}
