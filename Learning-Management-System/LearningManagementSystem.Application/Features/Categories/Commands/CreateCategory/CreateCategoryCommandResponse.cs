using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandResponse : BaseResponse
    {
        public CreateCategoryCommandResponse() : base()
        {
        }

        public CreateCategoryDto Category { get; set; } = default!;
    }
}
