using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(LearningManagementSystemDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Result<Question>> FindByIdAsync(Guid id)
        {
            var result = await context.Questions
                .Include(q => q.Choices)
                .FirstOrDefaultAsync(q => q.QuestionId == id);

            if (result == null)
            {
                return Result<Question>.Failure($"Question with id {id} not found.");
            }

            return Result<Question>.Success(result);
        }
    }
}
