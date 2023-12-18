using LearningManagementSystem.Domain.Common;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Rating : AuditableEntity
    {
        public Guid RatingId { get; private set; }
        public Guid EnrollmentId { get; private set; }
        public decimal Value { get; private set; }

        private Rating(Guid enrollmentId, decimal value)
        {
            RatingId = Guid.NewGuid();
            EnrollmentId = enrollmentId;
            Value = value;
        }

        public static Result<Rating> Create(Guid enrollmentId, decimal value)
        {
            if (enrollmentId == default)
            {
                return Result<Rating>.Failure("Enrollment Id is required");
            }

            if (value < 0 || value > 5)
            {
                return Result<Rating>.Failure("Invalid rating value");
            }

            return Result<Rating>.Success(new Rating(enrollmentId, value));
        }
    }
}
