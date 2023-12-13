using LearningManagementSystem.Application.Contracts.Identity;
using LearningManagementSystem.Application.Models.Identity;
using LearningManagementSystem.Identity.Models;

namespace LearningManagementSystem.Identity.Services
{
    public class InvalidRoleRegistrationServiceStrategy : IRegistrationServiceStrategy
    {
        public async Task<(int status, string message)> Registration(RegistrationModel model)
        {
            return (UserAuthenticationStatus.REGISTRATION_FAIL, "Invalid Role");
        }
    }
}
