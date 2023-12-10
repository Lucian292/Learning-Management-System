using LearningManagementSystem.Application.Contracts.Identity;
using LearningManagementSystem.Application.Models.Identity;

namespace LearningManagementSystem.Identity.Services
{
    public class InvalidRoleRegistrationServiceStrategy : IRegistrationServiceStrategy
    {
        public async Task<(int status, string message)> Registration(RegistrationModel model)
        {
            return (0, "Invalid Role");
        }
    }
}
