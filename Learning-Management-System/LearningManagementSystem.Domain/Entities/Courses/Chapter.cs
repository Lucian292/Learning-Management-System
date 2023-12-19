using LearningManagementSystem.Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Chapter : AuditableEntity
    {
        public Guid ChapterId { get; private set; }
        public Guid CourseId { get; private set; }
        public string Title { get; private set; }
        public string? Link { get; private set; }
        public List<Question> Quizz { get; private set; } = new();

        [MaxLength(20971520)] //dimensiunea maxima a unui fisier in baza de date este de 20 MB
        public byte[]? Content { get; private set; }
        public Course? Course { get; private set; }

        private Chapter(Guid courseId, string title)
        {
            this.ChapterId = Guid.NewGuid();
            this.CourseId = courseId;
            this.Title = title;
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

            return Result<Chapter>.Success(new Chapter(courseId, title));
        }


        public void Update(string title, string link)
        {
            this.Title = title;
            this.Link = link;
        }

        public void AttachContent(byte[] content)
        {
            if (content != null)
            {
                this.Content = content;
            }
        }

        public void AttachLink(string link)
        {
            if(!string.IsNullOrWhiteSpace(link))
                this.Link = link;
        }
    }
}
