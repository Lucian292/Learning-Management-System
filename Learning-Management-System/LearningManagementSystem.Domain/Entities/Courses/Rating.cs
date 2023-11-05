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
    }
}
