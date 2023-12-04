using LearningManagementSystem.Domain.Common;


namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Enrollment : AuditableEntity
    {
        public Guid EnrollmentId { get; set; }
        public string UserName { get; private set; } = string.Empty;
        public Guid CourseId { get; private set; }
        public decimal Progress { get; private set; }
        public Rating? Rating { get; private set; }
        public List<EnrollmentQuestionResult>? QuizzResults { get; set; }
        public Course? Course { get; private set; }

        private Enrollment(string userName, Guid courseId, decimal progress)
        {
            EnrollmentId = Guid.NewGuid();
            UserName = userName;
            CourseId = courseId;
            Progress = progress;
        }

        public static Result<Enrollment> Create(string userName, Guid courseId)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return Result<Enrollment>.Failure("User Name is required");
            }
            if (courseId == default)
            {
                return Result<Enrollment>.Failure("Course Id is required");
            }

            return Result<Enrollment>.Success(new Enrollment(userName, courseId, 0)); 
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
