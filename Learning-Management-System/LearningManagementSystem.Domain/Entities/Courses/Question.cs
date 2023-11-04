using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Question
    {
        public Guid QuestionId { get; private set; }
        public List<Choice> Choices { get; private set; }
        public Choice CorrectChoice { get; private set; }

        public Question(List<Choice> choices, Choice correctChoice)
        {
            QuestionId = Guid.NewGuid();
            Choices = choices;
            CorrectChoice = correctChoice;
        }
    }
}
