using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class ChoiceRepository : BaseRepository<Choice>, IChoiceRepository
    {
        public ChoiceRepository(LearningManagementSystemDbContext dbContext) : base(dbContext)
        { 
        }

        public override async Task<Result<Choice>> FindByIdAsync(Guid id)
        {
            var result = await context.Choices
                .Include(c => c.Question)
                .FirstOrDefaultAsync(q => q.ChoiceId == id);

            if (result == null)
            {
                return Result<Choice>.Failure($"Choice with id {id} not found.");
            }

            return Result<Choice>.Success(result);
        }
    }
}
