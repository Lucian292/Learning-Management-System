﻿using LearningManagementSystem.Application.Contracts.Identity;
using LearningManagementSystem.Identity.Models;
using LearningManagementSystem.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LearningManagementSystem.Identity
{
    public static class InfrastructureIdentityRegistrationDI
        {
            public static IServiceCollection AddInfrastrutureIdentityToDI(
                           this IServiceCollection services,
                                      IConfiguration configuration)
            {
                services.AddDbContext<ApplicationDbContext>(
                   options =>
                   options.UseNpgsql(
                       configuration.GetConnectionString
                       ("LearningManagementSystemUserConnection"),
                       builder =>
                       builder.MigrationsAssembly(
                           typeof(ApplicationDbContext)
                           .Assembly.FullName)));

                // For Identity  
                services.AddIdentity<ApplicationUser, IdentityRole>()
                                .AddEntityFrameworkStores<ApplicationDbContext>()
                                .AddDefaultTokenProviders();
                // Adding Authentication  
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })

                            // Adding Jwt Bearer  
                            .AddJwtBearer(options =>
                            {
                                options.SaveToken = true;
                                options.RequireHttpsMetadata = false;
                                options.TokenValidationParameters = new TokenValidationParameters()
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidAudience = configuration["JWT:ValidAudience"],
                                    ValidIssuer = configuration["JWT:ValidIssuer"],
                                    ClockSkew = TimeSpan.Zero,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                                };
                            });
            services.AddScoped<
                ILoginService, LoginService>();
            services.AddScoped<
                IRegistrationServiceStrategy, ProfessorRegistrationServiceStrategy>();
            services.AddScoped<
                IRegistrationServiceStrategy, StudentRegistrationServiceStrategy>();
            services.AddScoped<GetRegistrationStrategy>();
            services.AddScoped<
                IRegistrationServiceStrategy, InvalidRoleRegistrationServiceStrategy>();
            services.AddScoped<
                IUserService, UserService>();

            return services;
            }

        }
}
