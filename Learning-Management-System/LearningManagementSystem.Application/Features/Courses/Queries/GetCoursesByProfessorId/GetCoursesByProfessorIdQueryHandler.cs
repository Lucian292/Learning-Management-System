using MediatR;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Application.Contracts.Interfaces;

namespace LearningManagementSystem.Application.Features.Courses.Queries.GetByProfessorId
{
    public class GetCoursesByProfessorIdQueryHandler : IRequestHandler<GetCoursesByProfessorIdQuery, GetCoursesByProfessorIdQueryResponse>
    {
        private readonly ICourseRepository courseRepository;
        private readonly ICurrentUserService currentUserService;

        public GetCoursesByProfessorIdQueryHandler(ICourseRepository courseRepository, ICurrentUserService currentUserService)
        {
            this.courseRepository = courseRepository;
            this.currentUserService = currentUserService;
        }

        public async Task<GetCoursesByProfessorIdQueryResponse> Handle(GetCoursesByProfessorIdQuery request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(currentUserService.UserId);

            GetCoursesByProfessorIdQueryResponse response = new();

            var result = await courseRepository.GetCoursesByProfessorIdAsync(userId);

            if (result.IsSuccess)
            {
                response.Courses = result.Value.Select(c => new CourseDto
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    Description = c.Description,
                    UserId = c.ProfessorId,
                    CategoryId = c.CategoryId,
                    Chapters = c.Chapters.Select(c => new Chapters.Queries.ChapterDto
                    {
                        ChapterId = c.ChapterId,
                        Title = c.Title,
                        CourseId = c.CourseId,
                        //Link = c.Link,
                        //Content = c.Content
                    }).ToList()
                }).ToList();
            }
            return response;
        }
    }
}
