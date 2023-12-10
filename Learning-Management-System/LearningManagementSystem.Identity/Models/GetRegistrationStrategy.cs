using LearningManagementSystem.Application.Contracts.Identity;
using LearningManagementSystem.Identity.Services;
using Microsoft.AspNetCore.Identity;

namespace LearningManagementSystem.Identity.Models
{

    public class GetRegistrationStrategy
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetRegistrationStrategy(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IRegistrationServiceStrategy GetRegistrationRoleStrategy(string role)
        {
            switch (role)
            {
                case UserRoles.Student:
                    return new StudentRegistrationServiceStrategy(_userManager, _roleManager);
                case UserRoles.Professor:
                    return new ProfessorRegistrationServiceStrategy(_userManager, _roleManager);
                default:
                    return new InvalidRoleRegistrationServiceStrategy();
            }
        }
    }
}
