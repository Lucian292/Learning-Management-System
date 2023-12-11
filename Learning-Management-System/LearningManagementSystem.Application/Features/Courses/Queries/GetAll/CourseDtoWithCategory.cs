using LearningManagementSystem.Application.Features.Categories.Queries;
using LearningManagementSystem.Application.Features.Chapters.Queries;

namespace LearningManagementSystem.Application.Features.Courses.Queries.GetAll
{
    public class CourseDtoWithCategory
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public CategoryDto? Category { get; set; }
        public List<ChapterDto> Chapters { get; set; } = new();
    }
}