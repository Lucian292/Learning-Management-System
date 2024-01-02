using LearningManagementSystem.Domain.Common;
using System.Diagnostics.CodeAnalysis;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    [ExcludeFromCodeCoverage]
    public class CourseTag : AuditableEntity
    {
        public Guid CourseId { get; set; }
        public Guid TagId { get; set; }

        public required Course Course { get; set; }
        public required Tag Tag { get; set; }
    }
}
