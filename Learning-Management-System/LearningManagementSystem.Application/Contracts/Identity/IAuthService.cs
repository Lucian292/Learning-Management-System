﻿using LearningManagementSystem.Application.Models.Identity;

namespace LearningManagementSystem.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<(int, string)> Registration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
    }
}