using LearningManagementSystem.Domain.Common;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Tag : AuditableEntity
    {
        public Guid TagId { get; private set; }
        public string? Content { get; private set; }
        public List<CourseTag>? CourseTags { get; private set; }

        private Tag(string content)
        {
            TagId = Guid.NewGuid();
            Content = content;
        }

        public static Result<Tag> Create(Guid courseId, string content)
        {
            if (courseId == Guid.Empty)
            {
                return Result<Tag>.Failure("Course Id is required");
            }

            if (string.IsNullOrEmpty(content))
            {
                return Result<Tag>.Failure("Content is required");
            }

            return Result<Tag>.Success(new Tag(content));
        }

        public void Update(string content)
        {
            Content = content;
        }
    }
}
