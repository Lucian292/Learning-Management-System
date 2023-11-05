using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence;
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
