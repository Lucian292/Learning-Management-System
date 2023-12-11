namespace LearningManagementSystem.App.ViewModels
{
    public class CourseDto
    {
        public Guid CourseId { get; set; }
        public Guid CategoryId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public List<ChapterDto> Chapters { get; set; } = new List<ChapterDto>();
    }
}
