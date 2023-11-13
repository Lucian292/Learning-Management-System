
using LearningManagementSystem.Application.Features.Choice.Queries;

namespace LearningManagementSystem.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionDto
    {
        public Guid QuestionId { get; set; }
        public string? Text { get; set; }
        public List<ChoiceDto> Choices = default!;

    }
}
