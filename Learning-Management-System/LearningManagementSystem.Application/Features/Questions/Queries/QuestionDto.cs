using LearningManagementSystem.Application.Features.Choice.Queries;

namespace LearningManagementSystem.Application.Features.Questions.Queries.GetQuestionById
{
    public class QuestionDto
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = default!;

        public List<ChoiceDto> Choices = default!;

    }
}
