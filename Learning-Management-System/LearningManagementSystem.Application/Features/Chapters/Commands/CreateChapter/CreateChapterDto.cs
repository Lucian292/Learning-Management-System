namespace LearningManagementSystem.Application.Features.Chapters.Commands.CreateChapter
{
    public class CreateChapterDto
    {
        public Guid ChapterId { get; set; }
        public Guid CourseId { get; set; }
        public string? Title { get; set; }
        //public string? Link { get; set; }
        //public byte[]? Content { get; set; }
    }
}
