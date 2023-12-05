using LearningManagementSystem.Application.Persistence;
using LearningManagementSystem.Application.Responses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Categories.Queries.GetById
{
    public class GetByIdCategoryHandler : IRequestHandler<GetByIdCategoryQuery, CategoryDto>
    {
        private readonly ICategoryRepository repository;

        public GetByIdCategoryHandler(ICategoryRepository repository)
        {
            this.repository = repository;
        }
        public async Task<CategoryDto> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await repository.FindByIdAsync(request.Id);
            if (category.IsSuccess)
            {
                return new CategoryDto
                {
                    CategoryId = category.Value.CategoryId,
                    CategoryName = category.Value.CategoryName,
                    Description = category.Value.Description,
                    Courses = category.Value.Courses.Select(c => new Courses.Queries.CourseDto
                    {
                        CourseId = c.CourseId,
                        //CategoryId = c.CategoryId,
                        Description = c.Description,
                        Title = c.Title,
                        UserId = c.ProfessorId
                    }).ToList()
                };
            }


            return new CategoryDto();
        }
    }
}
