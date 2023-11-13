using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Application.Persistence.Users;
using LearningManagementSystem.Infrastructure.Repositories;
using LearningManagementSystem.Infrastructure.Repositories.Courses;
using LearningManagementSystem.Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearningManagementSystem.Infrastructure.Data
{
    public static class InfrastructureRegistrationDI
    {
        public static IServiceCollection AddInfrastrutureToDI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<LearningManagementSystemDbContext>(
                options =>
                options.UseNpgsql(
                    configuration.GetConnectionString
                    ("LearningManagementSystemConnection"),
                    builder =>
                    builder.MigrationsAssembly(
                        typeof(LearningManagementSystemDbContext)
                        .Assembly.FullName)));
            services.AddScoped
                (typeof(IAsyncRepository<>),
                typeof(BaseRepository<>));
            services.AddScoped<
                ICategoryRepository, CategoryRepository>();
            services.AddScoped<
                IUserRepository, UserRepository>();
            services.AddScoped<
                IQuestionRepository, QuestionRepository>();
            services.AddScoped<
                IChoiceRepository, ChoiseRepository>();
            return services;
        }
    }
}
