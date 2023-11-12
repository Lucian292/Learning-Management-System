using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;

namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class EnrollmentQuestionResultRepository: BaseRepository<EnrollmentQuestionResult>, IEnrollmentQuestionResultRepository
    {
        public EnrollmentQuestionResultRepository(LearningManagementSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
