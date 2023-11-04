using LearningManagementSystem.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class QuestionResult
    {
        public Guid QuestionResultId { get; private set; }
        public User Student { get; private set; }
        public Question Question { get; private set; }

        public QuestionResult(User student, Question question)
        {
            QuestionResultId = Guid.NewGuid();
            Student = student;
            Question = question;
        }
    }
}
