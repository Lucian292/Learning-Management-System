using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Asn1;
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

        public static Result<Course> Create(string title, string description, Guid userId, Guid categoryId)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result<Course>.Failure("Title is required");
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                return Result<Course>.Failure("Description is required");
            }
            if (userId == default)
            {
                return Result<Course>.Failure("User Id is required");
            }
            if (categoryId == default)
            {
                return Result<Course>.Failure("Category Id is required");
            }
            return Result<Course>.Success(new Course(title, description, userId, categoryId));
        }

        public void AttachTag(string tag)
        {
            if (!string.IsNullOrWhiteSpace(tag))
            {
                if (Tags == null)
                {
                    Tags = new List<string> { tag };
                }
                else
                {
                    Tags.Add(tag);
                }
            }
        }

        public void AttachEnrolledStudent(Enrollment newEnrollment)
        {
            if (newEnrollment != null)
            {
                if (EnrolledStudents == null)
                {
                    EnrolledStudents = new List<Enrollment> { newEnrollment };
                }
                else
                {
                    EnrolledStudents.Add(newEnrollment);
                }
            }
        }

        public void AttachChapters(Chapter chapter)
        {
            if (chapter != null)
            {
                if (Chapters == null)
                {
                    Chapters = new List<Chapter> { chapter };
                }
                else
                {
                    Chapters.Add(chapter);
                }
            }
        }
    }
}
