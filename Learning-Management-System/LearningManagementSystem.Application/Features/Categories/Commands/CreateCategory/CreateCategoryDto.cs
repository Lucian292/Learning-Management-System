namespace LearningManagementSystem.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryDto
    {
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
