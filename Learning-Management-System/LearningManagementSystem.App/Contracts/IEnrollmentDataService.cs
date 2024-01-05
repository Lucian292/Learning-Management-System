using LearningManagementSystem.App.ViewModels;

namespace LearningManagementSystem.App.Contracts
{
    public interface IEnrollmentDataService
    {
        Task<List<EnrolledCourseDto>> GetEnrolledCoursesAsync(Guid userId);
        Task<EnrolledCourseDto> CreateEnrollmentAsync(EnrolledCourseDto enrollment);
    }
}
