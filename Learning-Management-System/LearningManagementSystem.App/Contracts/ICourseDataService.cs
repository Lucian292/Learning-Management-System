using LearningManagementSystem.App.Services.Responses;
using LearningManagementSystem.App.ViewModels;

namespace LearningManagementSystem.App.Contracts
{
    public interface ICourseDataService
    {
        Task<List<CourseDto>> GetCoursesAsync();

        Task<ApiResponse<CourseDto>> CreateCourseAsync(CourseViewModel courseViewModel);

        Task<ApiResponse<CourseDto>> GetChaptersByCourseAsync(Guid courseId);
    }
}
