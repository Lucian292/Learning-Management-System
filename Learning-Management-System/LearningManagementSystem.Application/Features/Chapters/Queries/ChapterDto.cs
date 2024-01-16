using LearningManagementSystem.Application.Features.Courses.Queries;
using LearningManagementSystem.Application.Features.Questions.Queries.GetQuestionById;

namespace LearningManagementSystem.Application.Features.Chapters.Queries
{
    public class ChapterDto
    {
        public Guid ChapterId { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; } = default!;
        public string? Link { get; set; }
        public byte[]? Content { get; set; }
        public List<QuestionDto>? Questions { get; set; }
        public CourseDto? Course { get; set; }
    }
}
