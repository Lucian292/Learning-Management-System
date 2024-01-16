using MediatR;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.SolveQuiz
{
    public class SolveQuizCommand : IRequest<SolveQuizCommandResponse>
    {
        public Guid ChapterId { get; set; }
        public Guid EnrollmentId {  get; set; }
        public List<QuestionResultDto> QuestionResults { get; set; } = [];
    }
}
