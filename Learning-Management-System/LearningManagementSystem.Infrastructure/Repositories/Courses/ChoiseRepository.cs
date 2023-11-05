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
    public class ChoiseRepository : BaseRepository<Choice>, IChoiceRepository
    {
        public ChoiseRepository(LearningManagementSystemDbContext dbContext) : base(dbContext)
        { }
    }
}
