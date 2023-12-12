using LearningManagementSystem.App.Services.Responses;
using LearningManagementSystem.App.ViewModels;

namespace LearningManagementSystem.App.Contracts
{
    public interface ICategoryDataService
    {
        Task<List<CategoryViewModel>> GetCategoriesAsync();
        Task<ApiResponse<CategoryDto>> CreateCategoryAsync(CategoryViewModel categoryViewModel);

        Task<ApiResponse<CategoryDto>> GetCoursesByCategoryAsync(Guid CategoryId);
        Task<ApiResponse<CategoryDto>> UpdateCategoryAsync(CategoryViewModel updatedCategoryViewModel);
        Task<ApiResponse<CategoryViewModel>> GetCategoryByIdAsync(Guid categoryId);
    }
}
