using LearningManagementSystem.Application.Features.Courses.Queries;

namespace LearningManagementSystem.Application.Features.Categories.Queries
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
        public string? Description { get; set; }
        public List<CourseDto>? Courses { get; set; }
    }
}
