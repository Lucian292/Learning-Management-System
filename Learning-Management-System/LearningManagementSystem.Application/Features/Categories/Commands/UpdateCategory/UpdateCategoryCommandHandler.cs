using LearningManagementSystem.Application.Persistence;
using MediatR;

namespace LearningManagementSystem.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
    {
        private readonly ICategoryRepository categoryRepository;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindByIdAsync(request.CategoryId);

            if (!category.IsSuccess)
            {
                return new UpdateCategoryCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { category.Error }
                };
            }

            var updateCategoryDto = request.UpdateCategoryDto;

            category.Value.UpdateCategory(updateCategoryDto.CategoryName, updateCategoryDto.Description);
            
            await categoryRepository.UpdateAsync(category.Value);

            return new UpdateCategoryCommandResponse
            {
                Success = true,
                UpdatedCategory = new UpdateCategoryDto
                {
                    CategoryName = category.Value.CategoryName,
                    Description = category.Value.Description
                }
            };
        }
    }
}
