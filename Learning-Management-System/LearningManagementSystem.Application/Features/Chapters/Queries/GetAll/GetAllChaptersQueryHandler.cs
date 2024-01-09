using MediatR;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Application.Features.Courses.Queries;

namespace LearningManagementSystem.Application.Features.Chapters.Queries.GetAll
{
    public class GetAllChaptersQueryHandler : IRequestHandler<GetAllChaptersQuery, GetAllChaptersResponse>
    {
        private readonly IChapterRepository repository;

        public GetAllChaptersQueryHandler(IChapterRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllChaptersResponse> Handle(GetAllChaptersQuery request, CancellationToken cancellationToken)
        {
            GetAllChaptersResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Chapters = result.Value.Select(chapter => new ChapterDtoWithCourse
                {
                    ChapterId = chapter.ChapterId,
                    Title = chapter.Title,
                    Course = new CourseDto
                    {
                        CourseId = chapter.Course.CourseId,
                        Title = chapter.Course.Title,
                    },
                    Link = chapter.Link,
                    //Content = chapter.Content,
                }).ToList();
                
            }
            return response;
        }
    }
}
