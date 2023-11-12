using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Users;
using LearningManagementSystem.Domain.Entities.Users;
using LearningManagementSystem.Infrastructure.Data;


namespace LearningManagementSystem.Infrastructure.Repositories.Users
{
    public class UserRepository: BaseRepository<User>,IUserRepository
    {
        public UserRepository(LearningManagementSystemDbContext dbContext) : base(dbContext)
        {

        }
    }
}
