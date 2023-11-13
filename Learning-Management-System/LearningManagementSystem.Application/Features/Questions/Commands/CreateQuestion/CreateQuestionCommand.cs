using LearningManagementSystem.Application.Features.Choice.Queries;
using MediatR;

namespace LearningManagementSystem.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommand : IRequest<CreateQuestionCommandResponse>
    {
        public string Text { get; set; } = default!;
        public List<ChoiceDto> Answers { get; set; } = default!;
    }
}
