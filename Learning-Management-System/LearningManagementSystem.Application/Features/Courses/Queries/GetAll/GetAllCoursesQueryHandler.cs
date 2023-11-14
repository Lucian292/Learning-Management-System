using LearningManagementSystem.Application.Features.Categories.Queries.GetAll;
using LearningManagementSystem.Application.Features.Categories.Queries;
using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Courses.Queries.GetAll
{
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, GetAllCoursesQueryResponse>
    {
        private readonly ICourseRepository repository;

        public GetAllCoursesQueryHandler(ICourseRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllCoursesQueryResponse> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            GetAllCoursesQueryResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Courses = result.Value.Select(c => new CourseDto
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    Description = c.Description,
                    UserId = c.UserId,
                    CategoryId = c.CategoryId,
                }).ToList();
            }
            return response;
        }
    }
}
