using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;


namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class RatingRepository : BaseRepository<Rating>, IRatingRepository
    {
        public RatingRepository(LearningManagementSystemDbContext context) : base(context)
        {
        }
    }
}
