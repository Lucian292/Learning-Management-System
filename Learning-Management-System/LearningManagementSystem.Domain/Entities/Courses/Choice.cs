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

        private Choice(string content, Guid questionId, bool isCorrect)
        {
            ChoiceId = Guid.NewGuid();
            this.QuestionId = questionId;
            Content = content;
            IsCorrect = isCorrect;
        }

        public static Result<Choice> Create(string content, Guid questionId, bool isCorrect)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return Result<Choice>.Failure("Content is required");
            }
            if (questionId == default)
            {
                return Result<Choice>.Failure("Question Id can't be null");
            }
            return Result<Choice>.Success(new Choice(content, questionId, isCorrect));
        }
    }
}
