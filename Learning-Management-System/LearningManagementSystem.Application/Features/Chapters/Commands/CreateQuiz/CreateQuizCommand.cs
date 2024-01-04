using MediatR;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.CreateQuiz
{
    public class CreateQuizCommand : IRequest<CreateQuizCommandResponse>
    {
        public Guid ChapterId { get; set; }
        public List<QuestionDto> QuestionList { get; set; } = [];
    }
}
