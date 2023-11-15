namespace LearningManagementSystem.Application.Features.Chapters.Queries
{
    public class ChapterDto
    {
        public Guid ChapterId { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; } = default!;
        public string Link { get; set; } = default!;
        public byte[] Content { get; set; } = default!;
    }
}
