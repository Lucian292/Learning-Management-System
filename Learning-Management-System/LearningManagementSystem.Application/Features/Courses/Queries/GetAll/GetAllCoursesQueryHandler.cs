using LearningManagementSystem.Application.Features.Categories.Queries.GetAll;
using LearningManagementSystem.Application.Features.Categories.Queries;
using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Application.Features.Chapters.Queries;

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
                response.Courses = result.Value.Select(c => new CourseDtoWithCategory
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    Description = c.Description,
                    UserId = c.ProfessorId,
                    Category = new CategoryDto
                    {
                        CategoryId = c.Category.CategoryId,
                        CategoryName = c.Category.CategoryName
                    },
                    Chapters = c.Chapters.Select(c => new Chapters.Queries.ChapterDto
                    {
                        ChapterId = c.ChapterId,
                        Title = c.Title,
                        CourseId = c.CourseId,
                    }).ToList()
                }).ToList();

            }
            return response;
        }
    }
}
