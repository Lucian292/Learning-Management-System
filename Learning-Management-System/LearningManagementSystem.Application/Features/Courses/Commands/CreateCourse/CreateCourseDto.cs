namespace LearningManagementSystem.Application.Features.Couerses.Commands.CreateCourse
{
    public class CreateCourseDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
    }
}
