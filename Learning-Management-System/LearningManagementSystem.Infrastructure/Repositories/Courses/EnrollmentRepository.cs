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

    }
}
