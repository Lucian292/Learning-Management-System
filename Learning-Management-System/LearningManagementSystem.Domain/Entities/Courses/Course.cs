using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Course : AuditableEntity
    {
        public Guid CourseId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public Guid UserId { get; private set; }
        public Guid CategoryId { get; private set; }
        public List<string>? Tags { get; private set; }
        public List<Enrollment>? EnrolledStudents { get; private set; }
        public List<Chapter>? Chapters { get; private set; }

        private Course(string title, string description, Guid userId, Guid categoryId)
        {
            CourseId = Guid.NewGuid();
            Title = title;
            Description = description;
            UserId = userId;
            CategoryId = categoryId;
        }
    }
}
