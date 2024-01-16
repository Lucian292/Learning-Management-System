using LearningManagementSystem.App.Services.Responses;
using LearningManagementSystem.App.ViewModels;

namespace LearningManagementSystem.App.Contracts
{
    public interface IQuizDataService
    {
        Task<ApiResponse<QuizDto>> CreateQuizAsync(CreateQuizViewModel quizViewModel);
        Task<ApiResponse<SolveQuizDto>> SolveQuizAsync(SolveQuizViewModel quizViewModel);
        Task<ApiResponse<DeleteQuizDto>> DeleteQuizAsync(DeleteQuizViewModel deleteQuizViewModel);
        Task<List<QuizResultDto>> GetQuizResultsAsync(GetQuizResultsViewModel getQuizResults);
    }
}
