using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.Tags.Commands.CreateTag
{
    public class CreateTagCommandResponse : BaseResponse
    {
        public CreateTagDto? Tag { get; set; }
        
    }
}
