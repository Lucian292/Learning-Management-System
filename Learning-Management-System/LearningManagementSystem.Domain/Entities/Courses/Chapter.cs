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
    }
}
