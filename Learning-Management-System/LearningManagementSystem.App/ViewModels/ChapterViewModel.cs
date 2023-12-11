using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.App.ViewModels
{
    public class ChapterViewModel
    {
        public Guid Chapterid { get; set; }
        [Required(ErrorMessage ="Chapter title is required")]
        [StringLength(50, ErrorMessage ="The Chapter title should have maximum 50 characters")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage ="Course Id is required")]
        public Guid CourseId { get; set; }
    }
}
