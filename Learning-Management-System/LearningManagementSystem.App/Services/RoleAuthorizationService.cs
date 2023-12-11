using System.Security.Claims;

namespace LearningManagementSystem.App.Services
{
    public class RoleAuthorizationService
    {
        public bool IsUserAdmin(ClaimsPrincipal user)
        {
            return user.IsInRole("Admin");
        }
    }
}
