namespace LearningManagementSystem.App.ViewModels
{
    public class CreateQuizViewModel
    {
        public Guid ChapterId { get; set; }
        public List<CreateQuizQuestionViewModel> QuestionList { get; set; } = Enumerable.Range(1, 10)
                                                                                 .Select(i => new CreateQuizQuestionViewModel())
                                                                                 .ToList();
    }
}
