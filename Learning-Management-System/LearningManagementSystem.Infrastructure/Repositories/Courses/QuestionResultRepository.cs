using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class QuestionResultRepository : BaseRepository<QuestionResult>, IQuestionResultRepository
    {
        public QuestionResultRepository(LearningManagementSystemDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Result<IReadOnlyList<QuestionResult>>> GetByUserIdAsync(Guid userId)
        {
            var questionResults = await context.QuestionResults
                                    .Where(questionResult => questionResult.UserId == userId)
                                    .ToListAsync();
            return Result<IReadOnlyList<QuestionResult>>.Success(questionResults);
        }
    }
}
