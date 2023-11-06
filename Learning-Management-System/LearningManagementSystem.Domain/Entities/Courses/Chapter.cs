using LearningManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Chapter : AuditableEntity
    {
        public Guid ChapterId { get; private set; }
        public Guid CourseId { get; private set; }
        public string Title { get; private set; }
        public string Link { get; private set; }
        public List<Question>? Quizz {  get; private set; }
        public byte[] Content { get; private set; }

        private Chapter(Guid courseId, string title, string link, byte[] content)
        {
            this.ChapterId = Guid.NewGuid();
            this.CourseId = courseId;
            this.Title = title;
            this.Link = link;
            this.Content = content;
        }

        public static Result<Chapter> Create(Guid courseId, string title, string link, byte[] content)
        {
            if (courseId == default)
            {
                return Result<Chapter>.Failure("Course id can't be null");
            }
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result<Chapter>.Failure("Title is required");
            }
            if (string.IsNullOrWhiteSpace(link))
            {
                return Result<Chapter>.Failure("Link is required");
            }
            if (content == null)
            {
                return Result<Chapter>.Failure("Content is required");
            }
            return Result<Chapter>.Success(new Chapter(courseId, title, link, content));
        }

        public void AttachQuestion(Question question)
        {
            if (question != null)
            {
                if (Quizz == null)
                {
                    Quizz = new List<Question> { question };
                }
                else
                {
                    Quizz.Add(question);
                }
            }
        }
    }
}
