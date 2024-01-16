using LearningManagementSystem.App.ViewModels;

namespace LearningManagementSystem.App.Contracts
{
    public interface IQuestionResultDataService
    {
        Task<List<QuestionResultDto>> GetQuestionResultByUserId(Guid userId);
    }
}
