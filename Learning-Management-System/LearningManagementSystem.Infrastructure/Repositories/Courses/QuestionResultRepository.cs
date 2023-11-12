using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;

namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class QuestionResultRepository : BaseRepository<QuestionResult>, IQuestionResultRepository
    {
        public QuestionResultRepository(LearningManagementSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
