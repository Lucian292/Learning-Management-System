using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Models.Identity;
using Microsoft.Identity.Web;
using System.Security.Claims;

namespace LearningManagementSystem.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public string UserId => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
        public string[] UserRoles => httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToArray() ?? Array.Empty<string>();

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal GetCurrentClaimsPrincipal()
        {
            if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.User != null)
            {
                return httpContextAccessor.HttpContext.User;
            }

            return null!;
        }

        public string GetCurrentUserId()
        {
            return GetCurrentClaimsPrincipal()?.GetObjectId()!;
        }

        public bool IsUserAdmin()
        {
            return httpContextAccessor.HttpContext?.User?.IsInRole("Admin") ?? false;
        }
    }
}
