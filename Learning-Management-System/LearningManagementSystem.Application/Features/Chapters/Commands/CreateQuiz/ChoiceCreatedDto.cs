namespace LearningManagementSystem.Application.Features.Chapters.Commands.CreateQuiz
{
    public class ChoiceCreatedDto
    {
        public Guid ChoiceId { get; set; }
        public ChoiceDto? Choice { get; set; }
    }
}