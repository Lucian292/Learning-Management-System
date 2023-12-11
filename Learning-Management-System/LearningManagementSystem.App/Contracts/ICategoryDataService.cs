using LearningManagementSystem.App.Services.Responses;
using LearningManagementSystem.App.ViewModels;

namespace LearningManagementSystem.App.Contracts
{
    public interface ICategoryDataService
    {
        Task<List<CategoryViewModel>> GetCategoriesAsync();

        Task<ApiResponse<CategoryDto>> CreateCategoryAsync(CategoryViewModel categoryViewModel);
    }
    
}
