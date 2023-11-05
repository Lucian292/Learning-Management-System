using LearningManagementSystem.Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Persistence
{
    public interface IQuestionResultRepository : IAsyncRepository<QuestionResult>
    {
    }
}
