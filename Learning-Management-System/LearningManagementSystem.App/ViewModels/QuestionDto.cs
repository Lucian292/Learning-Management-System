namespace LearningManagementSystem.App.ViewModels
{
    public class QuestionDto
    {
        public Guid QuestionId { get; set; }
        public Guid ChapterId { get; set; }
        public string? Text { get; set; }
        public List<ChoiceDto> Choices { get; set; } = new();
    }
}