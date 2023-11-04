using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Chapter
    {
        public Guid ChapterId { get; private set; }
        public Course Course { get; private set; }
        public string Title { get; private set; }
        public string Link { get; private set; }
        public List<Question> Quizz {  get; private set; }
        public byte[] Content { get; private set; }

        public Chapter(Course course, string title, string link, List<Question> quizz, byte[] content)
        {
            this.ChapterId = Guid.NewGuid();
            this.Course = course;
            this.Title = title;
            this.Link = link;
            this.Quizz = quizz;
            this.Content = content;
        }
    }
}
