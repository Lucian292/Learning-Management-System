namespace LearningManagementSystem.App.ViewModels
{
    public class SolveQuizViewModel
    {
        public Guid ChapterId { get; set; }
        public Guid EnrollmentId { get; set; }
        public List<SolveQuizQuestionResultViewModel> QuestionResults { get; set; } = [];
    }
}
