﻿using LearningManagementSystem.Application.Contracts.Interfaces;
using Microsoft.Identity.Web;
using System.Security.Claims;

namespace LearningManagementSystem.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string UserId => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
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
    }
}
