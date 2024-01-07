namespace LearningManagementSystem.App.ViewModels
{
    public class QuizChoiceDto
    {
        public Guid ChoiceId { get; set; }
        public CreateQuizChoiceViewModel? Choice { get; set; }
    }
}