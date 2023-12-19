namespace LearningManagementSystem.App.ViewModels
{
    public class ChoiceDto
    {
        public Guid ChoiceId { get; set; }
        public Guid QuestionId { get; set; }
        public string? Content { get; set; }
        public bool IsCorrect { get; set; }

    }
}