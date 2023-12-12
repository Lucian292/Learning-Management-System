using LearningManagementSystem.App.Services.Responses;
using LearningManagementSystem.App.ViewModels;

namespace LearningManagementSystem.App.Contracts
{
    public interface ICourseDataService
    {
        Task<List<CourseDto>> GetCoursesAsync();
        Task<ApiResponse<CourseDto>> CreateCourseAsync(CourseViewModel courseViewModel);
        Task<ApiResponse<CourseViewModel>> GetCourseByIdAsync(Guid courseId);
        Task<ApiResponse<CourseDto>> GetChaptersByCourseAsync(Guid courseId);
        Task<ApiResponse<CourseDto>> UpdateCourseAsync(CourseViewModel updatedCourseViewModel);
    }
}
