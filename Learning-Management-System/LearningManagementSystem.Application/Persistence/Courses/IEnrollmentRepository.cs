using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;


namespace LearningManagementSystem.Application.Persistence.Courses
{
    public interface IEnrollmentRepository : IAsyncRepository<Enrollment>
    {
        Task<Result<IReadOnlyList<Enrollment>>> GetEnrollmentsByUserIdAsync(Guid userId);
    }
}
