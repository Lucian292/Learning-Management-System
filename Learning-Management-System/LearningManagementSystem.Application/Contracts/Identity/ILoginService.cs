using LearningManagementSystem.Application.Models.Identity;

namespace LearningManagementSystem.Application.Contracts.Identity
{
    public interface ILoginService
    {
        Task<(int, string)> Login(LoginModel model);
    }
}
