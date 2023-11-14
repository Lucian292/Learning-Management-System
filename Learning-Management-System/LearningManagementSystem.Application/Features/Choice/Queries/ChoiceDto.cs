namespace LearningManagementSystem.Application.Features.Choice.Queries
{
    public class ChoiceDto
    {
        public Guid ChoiceId { get; set; }
        public Guid QuestionId { get; set; }
        public string Content { get; set; } = default!;
        public bool IsCorrect { get; set; }
    }
}
