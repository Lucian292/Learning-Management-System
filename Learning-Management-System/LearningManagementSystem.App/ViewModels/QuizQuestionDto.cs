namespace LearningManagementSystem.App.ViewModels
{
    public class QuizQuestionDto
    {
        public Guid QuestionId { get; set; }
        public CreateQuizQuestionViewModel? Choice { get; set; }
    }
}
