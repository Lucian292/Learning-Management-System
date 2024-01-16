using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.QuestionResult.Queries.GetByUserId
{
    public class GetByUserIdQuestionResultResponse : BaseResponse
    {
        public GetByUserIdQuestionResultResponse() : base()
        {
        }
        public List<QuestionResultDto> QuestionResults { get; set; } = [];
    }
}