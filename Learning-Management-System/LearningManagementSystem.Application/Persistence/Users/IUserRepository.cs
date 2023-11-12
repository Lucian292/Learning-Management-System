using LearningManagementSystem.Domain.Entities.Users;


namespace LearningManagementSystem.Application.Persistence.Users
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}
