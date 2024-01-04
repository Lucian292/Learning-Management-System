namespace LearningManagementSystem.Application.Features.Chapters.Commands.CreateQuiz
{
    public class QuestionDto
    {
        public string Question {  get; set; } = string.Empty;
        public List<ChoiceDto> Choices { get; set; } = [];
    }
}
