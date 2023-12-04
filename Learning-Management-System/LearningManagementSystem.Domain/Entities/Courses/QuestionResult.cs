using LearningManagementSystem.Domain.Common;


namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class QuestionResult : AuditableEntity
    {
        public Guid QuestionResultId { get; private set; }
        public Guid QuestionId { get; private set; }
        public string UserName { get; private set; } = string.Empty;
        public bool IsCorrect { get; private set; }

        private QuestionResult(Guid questionId, string userName, bool isCorrect)
        {
            QuestionResultId = Guid.NewGuid();
            QuestionId = questionId;
            UserName = userName;
            IsCorrect = isCorrect;
        }

        public static Result<QuestionResult> Create(Guid questionId, string userName, bool isCorrect)
        {
            if (questionId == default)
            {
                return Result<QuestionResult>.Failure("Question Id is required");
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                return Result<QuestionResult>.Failure("User Name is required");
            }

            return Result<QuestionResult>.Success(new QuestionResult(questionId, userName, isCorrect));
        }
    }
}
