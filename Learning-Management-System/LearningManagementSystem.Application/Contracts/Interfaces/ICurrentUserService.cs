﻿using LearningManagementSystem.Application.Models.Identity;
using System.Security.Claims;

namespace LearningManagementSystem.Application.Contracts.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        ClaimsPrincipal GetCurrentClaimsPrincipal();
        string GetCurrentUserId();
        bool IsUserAdmin();
    }
}
