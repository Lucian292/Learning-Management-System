using LearningManagementSystem.Domain.Common;


namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Chapter : AuditableEntity
    {
        public Guid ChapterId { get; private set; }
        public Guid CourseId { get; private set; }
        public string Title { get; private set; }
        public string? Link { get; private set; }
        public List<Question> Quizz { get; private set; } = new();
        public byte[]? Content { get; private set; }
        public Course? Course { get; private set; }

        private Chapter(Guid courseId, string title/*, string link, byte[] content*/)
        {
            this.ChapterId = Guid.NewGuid();
            this.CourseId = courseId;
            this.Title = title;
            /*this.Link = link;
            this.Content = content;*/
        }

        public static Result<Chapter> Create(Guid courseId, string title)
        {
            if (courseId == default)
            {
                return Result<Chapter>.Failure("Course Id is required");
            }
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result<Chapter>.Failure("Title is required");
            }
            /*if (string.IsNullOrWhiteSpace(link))
            {
                return Result<Chapter>.Failure("Link is required");
            }
            if (content == null)
            {
                return Result<Chapter>.Failure("Content is required");
            }*/
            return Result<Chapter>.Success(new Chapter(courseId, title));
        }

        public void AttachQuestion(Question question)
        {
            if (question != null)
            {
                this.Quizz.Add(question);
            }
        }

        public void Update(string title, string link/*, byte[] content*/)
        {
            this.Title = title;
            this.Link = link;
            /*this.Content = content;*/
        }

        public void AttachContent(byte[] content)
        {
            this.Content = content;
        }

        public void AttachLink(string link)
        {
            this.Link = link;
        }
    }
}
