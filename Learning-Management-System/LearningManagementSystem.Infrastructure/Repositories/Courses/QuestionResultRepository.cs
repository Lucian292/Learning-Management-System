using Infrastructure.Repositories;
using LearningManagementSystem.Application.Persistence;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Infrastructure.Repositories.Courses
{
    public class QuestionResultRepository : BaseRepository<QuestionResult>, IQuestionResultRepository
    {
        public QuestionResultRepository(LearningManagementSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
