using LearningManagementSystem.Application.Persistence;
using LearningManagementSystem.Domain.Entities;
using MediatR;

namespace LearningManagementSystem.Application.Features.Categories.Queries.GetAll
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, GetAllCategoriesResponse>
    {
        private readonly ICategoryRepository repository;

        public GetAllCategoriesQueryHandler(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllCategoriesResponse> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            GetAllCategoriesResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Categories = result.Value.Select(category => new CategoryDto
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                }).ToList();
            }
            return response;
        }
    }
}
