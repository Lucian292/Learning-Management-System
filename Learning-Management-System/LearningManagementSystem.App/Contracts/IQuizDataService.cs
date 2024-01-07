using LearningManagementSystem.App.Services.Responses;
using LearningManagementSystem.App.ViewModels;

namespace LearningManagementSystem.App.Contracts
{
    public interface IQuizDataService
    {
        Task<ApiResponse<QuizDto>> CreateQuizAsync(CreateQuizViewModel quizViewModel);
    }
}
