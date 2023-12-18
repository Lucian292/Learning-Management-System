using LearningManagementSystem.App.Services.Responses;
using LearningManagementSystem.App.ViewModels;

namespace LearningManagementSystem.App.Contracts
{
    public interface IUserDataService
    {
        Task<ApiResponse<UserDto>> GetUserInfoAsync();
    }
}
