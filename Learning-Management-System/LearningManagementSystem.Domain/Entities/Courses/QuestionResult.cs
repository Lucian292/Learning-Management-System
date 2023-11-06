using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class QuestionResult : AuditableEntity
    {
        public Guid QuestionResultId { get; private set; }
        public Guid QuestionId { get; private set; }
        public Guid UserId { get; private set; }
        public bool IsCorrect { get; private set; }

        private QuestionResult(Guid questionId, Guid userId, bool isCorrect)
        {
            QuestionResultId = Guid.NewGuid();
            QuestionId = questionId;
            UserId = userId;
            IsCorrect = isCorrect;
        }

        public static Result<QuestionResult> Create(Guid questionId, Guid userId, bool isCorrect)
        {
            if (questionId == default)
            {
                return Result<QuestionResult>.Failure("Question Id is required");
            }

            if (userId == default)
            {
                return Result<QuestionResult>.Failure("User Id is required");
            }

            return Result<QuestionResult>.Success(new QuestionResult(questionId, userId, isCorrect));
        }
    }
}
