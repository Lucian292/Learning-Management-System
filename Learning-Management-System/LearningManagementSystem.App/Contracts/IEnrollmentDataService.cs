using LearningManagementSystem.App.Services.Responses;
using LearningManagementSystem.App.ViewModels;

namespace LearningManagementSystem.App.Contracts
{
    public interface IEnrollmentDataService
    {
        Task<List<EnrolledCourseDto>> GetEnrolledCoursesAsync(Guid userId);
    }
}
