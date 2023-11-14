using LearningManagementSystem.Application.Features.Categories.Queries.GetById;
using LearningManagementSystem.Application.Features.Categories.Queries;
using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Courses.Queries.GetById
{
    public class GetByIdCourseQueryHandler : IRequestHandler<GetByIdCourseQuery, CourseDto>
    {
        private readonly ICourseRepository repository;

        public GetByIdCourseQueryHandler(ICourseRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CourseDto> Handle(GetByIdCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await repository.FindByIdAsync(request.Id);
            if (course.IsSuccess)
            {
                return new CourseDto
                {
                    CourseId = course.Value.CourseId,
                    Title = course.Value.Title,
                    Description = course.Value.Description,
                    UserId = course.Value.UserId,
                    CategoryId = course.Value.CategoryId,
                };
            }
            return new CourseDto();
        }
    }
}
