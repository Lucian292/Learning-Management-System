using LearningManagementSystem.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Contracts.Identity
{
    public interface IRegistrationServiceStrategy
    {
        public Task<(int status, string message)> Registration(RegistrationModel model);
    }
}
