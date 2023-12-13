using LearningManagementSystem.Application.Models.Identity;
namespace LearningManagementSystem.Application.Contracts.Identity
{
    public interface IRegistrationServiceStrategy
    {
        public Task<(int status, string message)> Registration(RegistrationModel model);
    }
}
