using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;


namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class ChapterRepository : BaseRepository<Chapter>, IChapterRepository
    {
        public ChapterRepository(LearningManagementSystemDbContext context) : base(context)
        {
        }
    }
}
