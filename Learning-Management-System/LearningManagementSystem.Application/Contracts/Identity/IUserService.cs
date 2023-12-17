using LearningManagementSystem.Application.Models.Identity;
using LearningManagementSystem.Domain.Common;

namespace LearningManagementSystem.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<Result<UserInfoModel>> GetCurrentUserInfoAsync(string userId);
    }
}
