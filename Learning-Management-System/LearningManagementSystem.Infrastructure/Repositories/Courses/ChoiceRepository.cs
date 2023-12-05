using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;

namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class ChoiceRepository : BaseRepository<Choice>, IChoiceRepository
    {
        public ChoiceRepository(LearningManagementSystemDbContext dbContext) : base(dbContext)
        { }
    }
}
