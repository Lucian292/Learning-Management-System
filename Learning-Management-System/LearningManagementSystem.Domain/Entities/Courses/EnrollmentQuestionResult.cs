using LearningManagementSystem.Domain.Common;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class EnrollmentQuestionResult : AuditableEntity
    {
        public Guid EnrollmentId { get; set; }
        public required Enrollment Enrollment { get; set; }
        public Guid ChapterId { get; set; }
        public required Chapter Chapter { get; set; }
        public Guid QuestionResultId { get; set; }
        public required QuestionResult QuestionResult { get; set; }
    }
}
