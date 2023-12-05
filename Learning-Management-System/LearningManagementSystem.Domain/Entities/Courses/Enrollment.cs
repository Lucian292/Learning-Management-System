using LearningManagementSystem.Domain.Common;


namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Enrollment : AuditableEntity
    {
        public Guid EnrollmentId { get; set; }
        public Guid UserId { get; private set; }
        public Guid CourseId { get; private set; }
        public decimal Progress { get; private set; }
        public Rating? Rating { get; private set; }
        public List<EnrollmentQuestionResult>? QuizzResults { get; set; }
        public Course? Course { get; private set; }

        private Enrollment(Guid userId, Guid courseId, decimal progress)
        {
            EnrollmentId = Guid.NewGuid();
            UserId = userId;
            CourseId = courseId;
            Progress = progress;
        }

        public static Result<Enrollment> Create(Guid userId, Guid courseId)
        {
            if (userId == default)
            {
                return Result<Enrollment>.Failure("User Name is required");
            }
            if (courseId == default)
            {
                return Result<Enrollment>.Failure("Course Id is required");
            }

            return Result<Enrollment>.Success(new Enrollment(userId, courseId, 0)); 
        }

        public void AttachRating(Rating rating)
        {
            if (rating != null)
            {
                Rating = rating;
            }
        }

        public void AttachQuestionResult(EnrollmentQuestionResult questionResult)
        {
            if (questionResult != null)
            {
                if (QuizzResults == null)
                {
                    QuizzResults = new List<EnrollmentQuestionResult> { questionResult };
                }
                else
                {
                    QuizzResults.Add(questionResult);
                }
            }
        }
    }


}
