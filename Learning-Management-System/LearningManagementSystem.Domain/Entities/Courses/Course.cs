using LearningManagementSystem.Domain.Common;


namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Course : AuditableEntity
    {
        public Guid CourseId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public string UserName { get; private set; } = string.Empty;
        public Guid CategoryId { get; private set; }
        public List<CourseTag>? CourseTags { get; private set; }
        public List<Enrollment>? EnrolledStudents { get; private set; }
        public List<Chapter> Chapters { get; private set; } = new();

        private Course(string title, string description, string userName, Guid categoryId)
        {
            CourseId = Guid.NewGuid();
            Title = title;
            Description = description;
            UserName = userName;
            CategoryId = categoryId;
        }

        public static Result<Course> Create(string title, string description, string userName, Guid categoryId)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result<Course>.Failure("Title is required");
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                return Result<Course>.Failure("Description is required");
            }
            if (string.IsNullOrWhiteSpace(userName))
            {
                return Result<Course>.Failure("User Name is required");
            }
            if (categoryId == default)
            {
                return Result<Course>.Failure("Category Id is required");
            }
            return Result<Course>.Success(new Course(title, description, userName, categoryId));
        }

        public void AttachEnrolledStudent(Enrollment newEnrollment)
        {
            if (newEnrollment != null)
            {
                if (EnrolledStudents == null)
                {
                    EnrolledStudents = new List<Enrollment> { newEnrollment };
                }
                else
                {
                    EnrolledStudents.Add(newEnrollment);
                }
            }
        }

        public void AttachChapters(Chapter chapter)
        {
            if (chapter != null)
            {
                if (Chapters == null)
                {
                    Chapters = new List<Chapter> { chapter };
                }
                else
                {
                    Chapters.Add(chapter);
                }
            }
        }
    }
}
