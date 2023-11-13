

using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.Choice.Commands.CreateChoice
{
    public class CreateChoiceCommandResponse : BaseResponse
    {
        public CreateChoiceCommandResponse() : base()
        {
        }

        public CreateChoiceDto Choice { get; set; }
    }
}
