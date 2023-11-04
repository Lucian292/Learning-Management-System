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
    public class Course
    {
        public Guid CourseId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public Professor Professor { get; private set; }
        public Category Category { get; private set; }
        public List<string>? Tags { get; private set; }
        public List<Enrollment>? EnrolledStudents { get; private set; }
        public List<Chapter> Chapters { get; private set; }

        public Course(string title, string description, Professor professor, Category category, List<string> tags, List<Enrollment> enrollments, List<Chapter> chapters)
        {
            CourseId = Guid.NewGuid();
            Title = title;
            Description = description;
            Professor = professor;
            Category = category;
            Tags = tags;
            EnrolledStudents = enrollments;
            Chapters = chapters;
        }
    }
}
