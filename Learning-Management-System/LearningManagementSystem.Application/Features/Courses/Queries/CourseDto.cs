using LearningManagementSystem.Application.Features.Chapters.Queries;

namespace LearningManagementSystem.Application.Features.Courses.Queries
{
    public class CourseDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public List<ChapterDto>? Chapters { get; set; }
    }
}
