using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.App.ViewModels
{
    public class CategoryViewModel
    {
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
