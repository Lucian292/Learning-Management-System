using LearningManagementSystem.Domain.Common;
using static System.Net.Mime.MediaTypeNames;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Question : AuditableEntity
    {
        public Guid QuestionId { get; private set; }
        public List<Choice> Choices { get; private set; } = new(); // Lista de variante de raspuns
        public Guid ChapterId { get; private set; }
        public Chapter? Chapter { get; set; }
        public string Text { get; private set; }

        // Constructor fără parametrul List pentru Entity Framework
        private Question(string text, Guid chapterId)
        {
            QuestionId = Guid.NewGuid();
            Text = text;
            ChapterId = chapterId;

        }

        /*private Question(string text, List<Choice>? choices = null)
        {
            QuestionId = Guid.NewGuid();
            Text = text;
            Choices = choices;
        }*/

        public static Result<Question> Create(string text, Guid chapterId)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return Result<Question>.Failure("Text is required");
            }
            if (chapterId == default)
            {
                return Result<Question>.Failure("Chapter Id is required");
            }
            return Result<Question>.Success(new Question(text, chapterId));
        }

        /*public void AttachChoice(Choice choice)
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
        }*/
    }
}
