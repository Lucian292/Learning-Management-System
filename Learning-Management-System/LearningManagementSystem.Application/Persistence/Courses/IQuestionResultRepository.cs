using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;

namespace LearningManagementSystem.Application.Persistence.Courses
{
    public interface IQuestionResultRepository : IAsyncRepository<QuestionResult>
    {
        Task<Result<IReadOnlyList<QuestionResult>>> GetByUserIdAsync(Guid userId);
    }
}
