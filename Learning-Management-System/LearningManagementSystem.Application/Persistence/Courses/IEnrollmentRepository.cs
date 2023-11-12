using LearningManagementSystem.Domain.Entities.Courses;


namespace LearningManagementSystem.Application.Persistence.Courses
{
    public interface IEnrollmentRepository : IAsyncRepository<Enrollment>
    {
    }
}
