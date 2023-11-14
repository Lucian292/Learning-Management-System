namespace LearningManagementSystem.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryDto
    {
        public string CategoryName { get; set; } = default!;
        public string? Description { get; set; }
    }
}
