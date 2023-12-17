using LearningManagementSystem.Application.Contracts.Identity;
using LearningManagementSystem.Application.Models.Identity;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace LearningManagementSystem.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<Result<UserInfoModel>> GetCurrentUserInfoAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Result<UserInfoModel>.Failure($"User with the id:{userId} was not found!");
            }

            return Result<UserInfoModel>.Success(
                    new UserInfoModel(
                        user.UserName,
                        user.Email,
                        user.PhoneNumber,
                        user.FirstName,
                        user.LastName
                    )
                );
        }
    }
}
