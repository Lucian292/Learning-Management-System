using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence;
using LearningManagementSystem.Domain.Entities.Users;
using LearningManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Infrastructure.Repositories.Users
{
    public class UserRepository: BaseRepository<User>,IUserRepository
    {
        public UserRepository(LearningManagementSystemDbContext dbContext) : base(dbContext)
        {

        }
    }
}
