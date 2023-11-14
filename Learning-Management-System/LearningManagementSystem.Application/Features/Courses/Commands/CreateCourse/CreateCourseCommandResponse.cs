

using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.Couerses.Commands.CreateCourse
{
    public class CreateCourseCommandResponse : BaseResponse
    {
        public CreateCourseCommandResponse() : base() 
        {
        }

        public CreateCourseDto Course { get; set; }
    }
}
