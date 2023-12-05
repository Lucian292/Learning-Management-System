using LearningManagementSystem.Application.Features.Tags.Queries;

namespace LearningManagementSystem.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
