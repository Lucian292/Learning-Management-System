using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(LearningManagementSystemDbContext context) : base(context)
        {
        }

        public override async Task<Result<Course>> FindByIdAsync(Guid id)
        {
            var result = await context.Courses.Include(c => c.Chapters)
                .FirstOrDefaultAsync(c => c.CourseId == id)!;
            if (result == null)
            {
                return Result<Course>.Failure($"Course with id {id} not found");
            }
            return Result<Course>.Success(result);
        }
    }
}
