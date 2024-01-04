using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.CreateQuiz
{
    public class CreateQuizCommandResponse : BaseResponse
    {
        public CreateQuizCommandResponse() : base()
        {
        }

        public QuizDto QuizDto { get; set; }
    }
}
