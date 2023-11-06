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

        public static Result<Question> Create(string text)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
                return Result<Question>.Failure("Text is required");
            }
            return Result<Question>.Success(new Question(text));
        }

        public void AttachChoice(Choice choice)
        {
            if (choice != null)
            {
                if (Choices == null)
                {
                    Choices = new List<Choice> { choice };
                }
                else
                {
                    Choices.Add(choice);
                }
            }
        }
    }
}
