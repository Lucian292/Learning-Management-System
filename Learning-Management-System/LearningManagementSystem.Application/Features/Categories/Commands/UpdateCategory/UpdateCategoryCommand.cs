using MediatR;

namespace LearningManagementSystem.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<UpdateCategoryCommandResponse>
    {
        public Guid CategoryId { get; set; }
        public UpdateCategoryDto UpdateCategoryDto { get; set; } = new UpdateCategoryDto();
    }
}
