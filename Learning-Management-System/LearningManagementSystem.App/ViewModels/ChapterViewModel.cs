namespace LearningManagementSystem.App.ViewModels
{
    public class ChapterViewModel
    {
        public Guid ChapterId { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; } = default!;
        public string Link { get; set; } = default!;
        public byte[] Content { get; set; } = default!;
    }
}
