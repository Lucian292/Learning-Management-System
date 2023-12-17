using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.Enrollments.Queries.GetByUserId
{
    public class GetEnrollmentsByUserIdResponse : BaseResponse
    {
        public GetEnrollmentsByUserIdResponse() : base()
        {
        }
        public List<EnrollmentDto> Enrollments { get; set; } = [];
    }
}
