using LearningManagementSystem.Domain.Common;


namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Course : AuditableEntity
    {
        public Guid CourseId { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; } = string.Empty;
        public Guid ProfessorId { get; private set; }
        public Guid CategoryId { get; private set; }
        public List<CourseTag> CourseTags { get; private set; } = new();
        public List<Enrollment> EnrolledStudents { get; private set; } = new();
        public List<Chapter> Chapters { get; private set; } = new();

        private Course(string title, string description, Guid professorId, Guid categoryId)
        {
            CourseId = Guid.NewGuid();
            Title = title;
            Description = description;
            ProfessorId = professorId;
            CategoryId = categoryId;
        }

        public static Result<Course> Create(string title, string description, Guid professorId, Guid categoryId)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result<Course>.Failure("Title is required");
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                return Result<Course>.Failure("Description is required");
            }
            if (professorId == default)
            {
                return Result<Course>.Failure("Professor Id is required");
            }
            if (categoryId == default)
            {
                return Result<Course>.Failure("Category Id is required");
            }
            return Result<Course>.Success(new Course(title, description, professorId, categoryId));
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

        public void Update(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public void AttachTags(Tag tag)
        {
            if (tag != null)
            {
                CourseTags.Add(new CourseTag(this.CourseId, tag.TagId));
            }
        }

        public void RemoveTags(Tag tag)
        {
            if (tag != null)
            {
                var courseTag = CourseTags.FirstOrDefault(x => x.TagId == tag.TagId);
                if (courseTag != null)
                {
                    CourseTags.Remove(courseTag);
                }
            }
        }
    }
}
