using LearningManagementSystem.Application.Contracts.Identity;
using LearningManagementSystem.Application.Models.Identity;
using LearningManagementSystem.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace LearningManagementSystem.Identity.Services
{
    public class StudentRegistrationServiceStrategy : IRegistrationServiceStrategy
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StudentRegistrationServiceStrategy(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task<(int status, string message)> Registration(RegistrationModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName!);
            if (userExists != null)
                return (UserAuthenticationStatus.REGISTRATION_FAIL, "Username already used");

            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };

            var createUserResult = await _userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
                return (UserAuthenticationStatus.REGISTRATION_FAIL, "User creation failed! Please check user details and try again.");

            if (!await _roleManager.RoleExistsAsync(model.Role))
                await _roleManager.CreateAsync(new IdentityRole(model.Role));

            if (await _roleManager.RoleExistsAsync(UserRoles.Student))
                await _userManager.AddToRoleAsync(user, model.Role);

            return (UserAuthenticationStatus.REGISTRATION_SUCCES, "User created successfully!");
        }
    }
}
