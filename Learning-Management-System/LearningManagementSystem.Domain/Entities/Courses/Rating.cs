using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Rating : AuditableEntity
    {
        public Guid RatingId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid CourseId { get; private set; }
        public decimal Value { get; private set; }

        private Rating(Guid userId, Guid courseId, decimal value)
        {
            RatingId = Guid.NewGuid();
            UserId = userId;
            CourseId = courseId;
            Value = value;
        }

        public static Result<Rating> Create(Guid userId, Guid courseId, decimal value)
        {
            if (userId == default)
            {
                return Result<Rating>.Failure("Question Id is required");
            }

            if (courseId == default)
            {
                return Result<Rating>.Failure("Course Id is required");
            }

            return Result<Rating>.Success(new Rating(userId, courseId, value));
        }
    }
}
