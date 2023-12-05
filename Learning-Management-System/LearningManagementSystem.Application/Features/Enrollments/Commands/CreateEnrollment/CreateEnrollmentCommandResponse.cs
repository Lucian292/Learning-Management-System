using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommandResponse : BaseResponse
    {
        public CreateEnrollmentCommandResponse() : base()
        {
        }

        public CreateEnrollmentDto? Enrollment { get; set; }
    }
}
