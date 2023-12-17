namespace LearningManagementSystem.App.ViewModels
{
    public class EnrolledCourseDto
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public decimal? Progress { get; set; }

        public CourseDto? Course { get; set; }
    }
}
