using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandResponse : BaseResponse
    {
        public CreateQuestionDto? Question { get; set; }
    }
}
