

namespace LearningManagementSystem.Application.Features.Choice.Commands.CreateChoice
{
    public class CreateChoiceDto
    {
        public Guid ChoiceId { get; set; }
        public Guid QuestionId { get; set; }
        public string? Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
