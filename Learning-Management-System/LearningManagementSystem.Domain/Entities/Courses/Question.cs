using LearningManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Question : AuditableEntity
    {
        public Guid QuestionId { get; private set; }
        public List<Choice>? Choices { get; private set; }

        public string Text { get; private set; }

        private Question(string text)
        {
            QuestionId = Guid.NewGuid();
            Text = text;
        }
    }
}
