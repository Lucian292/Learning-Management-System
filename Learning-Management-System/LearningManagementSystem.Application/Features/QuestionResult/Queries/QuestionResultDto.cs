namespace LearningManagementSystem.Application.Features.QuestionResult.Queries
{
    public class QuestionResultDto
    {
        public Guid QuestionResultId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
