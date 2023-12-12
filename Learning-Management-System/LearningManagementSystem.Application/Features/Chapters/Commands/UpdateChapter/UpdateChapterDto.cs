namespace LearningManagementSystem.Application.Features.Chapters.Commands.UpdateChapter
{
    public class UpdateChapterDto
    {
        public string? Title { get; set; }
        public string? Link { get; set; }
        public byte[]? Content { get; set; }
    }
}
