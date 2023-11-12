using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;


namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(LearningManagementSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
