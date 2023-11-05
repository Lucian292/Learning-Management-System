using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private Enrollment(Guid userId, Guid courseId, decimal progress)
        {
            EnrollmentId = Guid.NewGuid();
            UserId = userId;
            CourseId = courseId;
            Progress = progress;
        }
    }


}
