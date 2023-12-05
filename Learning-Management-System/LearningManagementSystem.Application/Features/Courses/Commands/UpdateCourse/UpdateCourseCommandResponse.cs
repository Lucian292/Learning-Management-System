using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandResponse : BaseResponse
    {
        public UpdateCourseDto? UpdateCourse { get; set; }
    }
}
