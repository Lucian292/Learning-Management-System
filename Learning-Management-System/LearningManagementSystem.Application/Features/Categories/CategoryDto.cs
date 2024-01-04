using LearningManagementSystem.Application.Features.Courses.Queries;

namespace LearningManagementSystem.Application.Features.Categories
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
    }
}
