using LearningManagementSystem.Application.Features.Courses.Queries;

namespace LearningManagementSystem.Application.Features.Categories.Queries.GetById
{
    public class GetSingleCategoryDto : CategoryDto
    {
        public List<CourseDto> Courses { get; set; } = [];
    }
}
