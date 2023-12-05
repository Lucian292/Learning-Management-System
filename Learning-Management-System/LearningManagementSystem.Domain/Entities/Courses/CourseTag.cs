using LearningManagementSystem.Domain.Common;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class CourseTag : AuditableEntity
    {
        public Guid CourseId { get; set; }
        public Guid TagId { get; set; }

        public Course Course { get; set; }
        public Tag Tag { get; set; }

        public CourseTag(Guid courseId, Guid tagId)
        {
            CourseId = courseId;
            TagId = tagId;
        }

        public static Result<CourseTag> Create(Guid courseId, Guid tagId)
        {
            if (courseId == default)
            {
                return Result<CourseTag>.Failure("Course Id is required");
            }
            if (tagId == default)
            {
                return Result<CourseTag>.Failure("Tag Id is required");
            }
            return Result<CourseTag>.Success(new CourseTag(courseId, tagId));
        }
    }
}
