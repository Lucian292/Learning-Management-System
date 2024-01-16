using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.Chapters.Queries.GetQuizResults
{
    public class GetQuizResultsQueryResponse : BaseResponse
    {
        public List<QuizResultDto> Results { get; set; } = [];

        public GetQuizResultsQueryResponse() : base()
        {
            
        }
    }
}
