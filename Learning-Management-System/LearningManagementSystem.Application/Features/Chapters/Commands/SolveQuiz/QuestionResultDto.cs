namespace LearningManagementSystem.Application.Features.Chapters.Commands.SolveQuiz
{
    public class QuestionResultDto
    {
        public Guid QuestionId { get; set; }
        public Guid ChoiceId { get; set; }
        public bool IsCorrect { get; set; }
    }
}