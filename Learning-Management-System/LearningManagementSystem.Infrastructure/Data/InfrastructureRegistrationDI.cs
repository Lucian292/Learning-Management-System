using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Infrastructure.Repositories;
using LearningManagementSystem.Infrastructure.Repositories.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearningManagementSystem.Infrastructure.Data
{
    public static class InfrastructureRegistrationDI
    {
        public static IServiceCollection AddInfrastructureToDI(
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
                ICourseRepository, CourseRepository>();
            services.AddScoped<
                IChapterRepository, ChapterRepository>();
            services.AddScoped<
                IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<
                IQuestionRepository, QuestionRepository>();
            services.AddScoped<
                IChoiceRepository, ChoiseRepository>();
            services.AddScoped<
                ITagRepository, TagRepository>();
            return services;
        }
    }
}
