using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.App.ViewModels
{
    public class CourseViewModel
    {
        public Guid CourseId { get; set; }
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "Course name is required")]
        [StringLength(50, ErrorMessage = "The Course name should have maximum 50 characters")]
        public string CourseName { get; set; } = string.Empty;
        [StringLength(300, ErrorMessage = "The Description should have maximum 300 characters")]
        public string? Description { get; set; }
        public Guid UserId { get; set; }
    }
}
