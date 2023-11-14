using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandResponse : BaseResponse
    {
        public UpdateCategoryDto? UpdatedCategory { get; set; }
    }
}
