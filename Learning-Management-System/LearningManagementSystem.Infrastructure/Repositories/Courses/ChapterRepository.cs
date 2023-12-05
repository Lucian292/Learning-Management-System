using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class ChapterRepository : BaseRepository<Chapter>, IChapterRepository
    {
        public ChapterRepository(LearningManagementSystemDbContext context) : base(context)
        {
        }

        public override async Task<Result<Chapter>> FindByIdAsync(Guid id)
        {
            var chapter = await context.Chapters
                .Include(c => c.Quizz)
                .Include(c => c.Course)
                .FirstOrDefaultAsync(c => c.ChapterId == id);

            if (chapter == null)
            {
                return Result<Chapter>.Failure($"Chapter with id {id} not found");
            }

            return Result<Chapter>.Success(chapter);
        }   
    }
}
