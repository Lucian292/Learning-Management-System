using LearningManagementSystem.Domain.Entities.Courses;


namespace LearningManagementSystem.Application.Persistence.Courses
{
    public interface ICourseRepository : IAsyncRepository<Course>
    {
    }
}
