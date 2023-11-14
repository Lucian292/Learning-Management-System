using MediatR;


namespace LearningManagementSystem.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommand : IRequest<DeleteCourseCommandResponse>
    {
        public Guid CourseId { get; set; }
    }
}
