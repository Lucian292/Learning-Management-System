namespace LearningManagementSystem.App.ViewModels
{
    public class QuizResultDto
    {
        public bool WasCorrect { get; set; }
        public string RightChoice { get; set; } = string.Empty;
        public string Question { get; set; } = string.Empty;
    }
}