namespace LearningManagementSystem.App.ViewModels
{
    public class CreateQuizQuestionViewModel
    {
        public string Question {  get; set; } = string.Empty;
        public List<CreateQuizChoiceViewModel> Choices { get; set; } = Enumerable.Range(1, 3)
                                                                                 .Select(i => new CreateQuizChoiceViewModel())
                                                                                 .ToList();
    }
}