using LearningManagementSystem.App.Services.Responses;
using LearningManagementSystem.App.ViewModels;

namespace LearningManagementSystem.App.Contracts
{
    public interface IChapterDataService
    {
        Task<List<ChapterDto>> GetChaptersAsync();
        Task<ApiResponse<ChapterDto>> CreateChapterAsync(ChapterViewModel chapterViewModel);
    }
}
