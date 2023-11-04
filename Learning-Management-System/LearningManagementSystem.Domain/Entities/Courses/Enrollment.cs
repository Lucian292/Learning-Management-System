using LearningManagementSystem.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Enrollment
    {
        public Guid EnrollmentId { get; set; }
        public User Student {  get; private set; }
        public Course Course { get; private set; }
        public decimal Progress { get; private set; }
        public Rating Rating { get; private set; }
        public Dictionary<Chapter, List<QuestionResult>> QuizzResults { get; set; }

        public Enrollment(User student, Course course, decimal progress, Rating rating)
        {
            EnrollmentId = Guid.NewGuid();
            Student = student;
            Course = course;
            Progress = progress;
            Rating = rating;
        }

    }
}
