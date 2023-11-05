using LearningManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Choice : AuditableEntity
    {
        public Guid ChoiceId { get; private set; }
        public string Content { get; private set;}
        public Guid QuestionId { get; private set; }

        public bool IsCorrect { get; private set; }

        private Choice(string content, Guid QuestionId, bool isCorrect)
        {
            ChoiceId = Guid.NewGuid();
            this.QuestionId = QuestionId;
            Content = content;
            IsCorrect = isCorrect;
        }
    }
}
