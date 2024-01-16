using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.App.ViewModels
{
    public class CourseDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public CategoryDto? Category { get; set; }
        public List<ChapterDto>? Chapters { get; set; }
    }
}
