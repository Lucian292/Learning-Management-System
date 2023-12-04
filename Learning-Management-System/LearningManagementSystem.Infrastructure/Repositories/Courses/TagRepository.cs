using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;

namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(LearningManagementSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
