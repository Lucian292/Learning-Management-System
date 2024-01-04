namespace LearningManagementSystem.Application.Features.Chapters.Commands.CreateQuiz
{
    public class QuestionCreatedDto
    {
        public Guid QuestionId { get; set; }
        public List<ChoiceCreatedDto> Choices { get; set; } = [];
    }
}
