using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;


namespace LearningManagementSystem.Application.Persistence.Courses
{
    public interface ICourseRepository : IAsyncRepository<Course>
    {
        Task<bool> IsCourseOwnedByUserAsync(Guid courseId, Guid userId);
        Task<Result<IReadOnlyList<Course>>> GetCoursesByProfessorIdAsync(Guid professorId);
    }
}
