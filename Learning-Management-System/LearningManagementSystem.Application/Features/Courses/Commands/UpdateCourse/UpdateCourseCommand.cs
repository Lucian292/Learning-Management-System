using LearningManagementSystem.Application.Features.Courses.Commands.UpdateCourse;
using MediatR;

namespace LearningManagementSystem.Application.Features.Courses.Commands.UpdateCourseCommand
{
    public class UpdateCourseCommand : IRequest<UpdateCourseCommandResponse>
    {
        public Guid CourseId { get; set; }
        public UpdateCourseDto UpdateCourseDto { get; set; } =new UpdateCourseDto();
    }
}
