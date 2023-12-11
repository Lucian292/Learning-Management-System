using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.App.ViewModels
{
    public class CourseViewModel
    {
        public Guid CourseId { get; set; }
        [Required(ErrorMessage = "Course name is required")]

        public string Title { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }

}
