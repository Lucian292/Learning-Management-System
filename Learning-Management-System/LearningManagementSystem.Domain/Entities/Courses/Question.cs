using LearningManagementSystem.Domain.Common;
using static System.Net.Mime.MediaTypeNames;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Question : AuditableEntity
    {
        public Guid QuestionId { get; private set; }
        public List<Choice>? Choices { get; private set; } // Lista de variante de raspuns

        public string Text { get; private set; }

        // Constructor fără parametrul List pentru Entity Framework
        private Question(string text)
        {
            QuestionId = Guid.NewGuid();
            Text = text;
        }

        private Question(string text, List<Choice>? choices = null)
        {
            QuestionId = Guid.NewGuid();
            Text = text;
            Choices = choices;
        }

        public static Result<Question> Create(string text, List<Choice>? choices = null)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return Result<Question>.Failure("Text is required");
            }
            return Result<Question>.Success(new Question(text, choices));
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
