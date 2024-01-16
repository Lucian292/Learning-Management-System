using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(LearningManagementSystemDbContext context) : base(context)
        {
        }

        public async Task<Result<IReadOnlyList<Enrollment>>> GetEnrollmentsByUserIdAsync(Guid userId)
        {
            var enrollments = await context.Enrollments
                                    .Where(enrollment => enrollment.UserId == userId)
                                    .ToListAsync();

            return Result<IReadOnlyList<Enrollment>>.Success(enrollments);
        }

        public override async Task<Result<Enrollment>> FindByIdAsync(Guid id)
        {
            var result = await context.Enrollments.Include(e => e.QuizzResults).ThenInclude(q => q.QuestionResult).ThenInclude(q => q.Question).ThenInclude(q => q.Choices).FirstOrDefaultAsync(e => e.EnrollmentId == id);
            if (result == null)
            {
                return Result<Enrollment>.Failure($"Entity with id {id} not found");
            }
            return Result<Enrollment>.Success(result);
        }
    }
}
