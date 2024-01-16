using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(LearningManagementSystemDbContext context) : base(context)
        {
        }

        public override async Task<Result<Course>> FindByIdAsync(Guid id)
        {
            var result = await context.Courses
                .Include(c => c.Chapters).ThenInclude(c => c.Quizz)
                .FirstOrDefaultAsync(c => c.CourseId == id)!;
            if (result == null)
            {
                return Result<Course>.Failure($"Course with id {id} not found");
            }
            return Result<Course>.Success(result);
        }

        public async Task<Result<IReadOnlyList<Course>>> GetCoursesByProfessorIdAsync(Guid professorId)
        {
            var courses = await context.Courses
                                .Where(course => course.ProfessorId == professorId)
                                .Include(c => c.Chapters)
                                .ToListAsync();

            return Result<IReadOnlyList<Course>>.Success(courses);
        }

        public async Task<bool> IsCourseOwnedByUserAsync(Guid courseId, Guid userId)
        {
            var course = await context.Courses.FindAsync(courseId);

            // Check if the course exists and if the user ID matches the course owner ID
            return course != null && course.ProfessorId == userId;
        }

        public override async Task<Result<IReadOnlyList<Course>>> GetAllAsync()
        {
            var result = await context.Courses.Include(c=>c.Category).AsNoTracking().ToListAsync();
            return Result<IReadOnlyList<Course>>.Success(result);
        }

    }
}
