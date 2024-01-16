using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.SolveQuiz
{
    public class SolveQuizCommandResponse : BaseResponse
    {
        public SolveQuizCommandResponse() : base() { }
        public List<EnrollmentQuestionResultDto> QuestionResults { get; set; } = [];
    }
}
