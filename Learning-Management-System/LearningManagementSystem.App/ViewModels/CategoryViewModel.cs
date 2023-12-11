using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.App.ViewModels
{
    public class CategoryViewModel
    {
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, ErrorMessage = "The Category name should have maximum 50 characters")]

        public string CategoryName { get; set; } = string.Empty;
    }
}
