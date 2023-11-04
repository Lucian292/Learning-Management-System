using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Choice
    {
        public Guid ChoiceId { get; private set; }
        public string Content { get; private set;}
        public Question Question { get; private set; }

        public Choice(string content, Question question)
        {
            ChoiceId = Guid.NewGuid();
            Content = content;
            Question = question;
        }
    }
}
