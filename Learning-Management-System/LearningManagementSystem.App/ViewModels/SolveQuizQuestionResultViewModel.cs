namespace LearningManagementSystem.App.ViewModels
{
    public class SolveQuizQuestionResultViewModel
    {
        public Guid QuestionId { get; set; }
        public Guid ChoiceId { get; set; }
        public bool IsCorrect { get; set; }
    }
}