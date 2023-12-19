namespace LearningManagementSystem.App.ViewModels
{
    public class ChapterDto
    {
        public Guid ChapterId { get; set; }
        public Guid CourseId { get; set; }
        public CourseDto? Course { get; set; }
        public string Title { get; set; } = default!;
        public string Link { get; set; } = default!;
        public byte[]? Content { get; set; }
        public List<QuestionDto> Questions { get; set; } = new();
    }
}
