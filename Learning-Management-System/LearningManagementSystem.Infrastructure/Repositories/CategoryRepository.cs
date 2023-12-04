using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(LearningManagementSystemDbContext context) : base(context)
        {
        }

        public override async Task<Result<Category>> FindByIdAsync(Guid id)
        {
            var result = await context.Categories
                .Include(c => c.Courses)
                .FirstOrDefaultAsync(c => c.CategoryId == id)!;
            if (result == null)
            {
                return Result<Category>.Failure($"Entity with id {id} not found");
            }
            return Result<Category>.Success(result);
        }
    }
}
