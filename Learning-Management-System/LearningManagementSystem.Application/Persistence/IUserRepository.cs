using LearningManagementSystem.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}
