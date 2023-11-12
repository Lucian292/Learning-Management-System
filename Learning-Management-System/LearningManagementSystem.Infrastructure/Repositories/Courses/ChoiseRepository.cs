using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;

namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class ChoiseRepository : BaseRepository<Choice>, IChoiceRepository
    {
        public ChoiseRepository(LearningManagementSystemDbContext dbContext) : base(dbContext)
        { }
    }
}
