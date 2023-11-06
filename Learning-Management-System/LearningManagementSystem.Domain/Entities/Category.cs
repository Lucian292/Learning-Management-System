using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public List<Course>? Courses { get; private set; }

        private Category(string name)
        {
            CategoryId = Guid.NewGuid();
            Name = name;
        }

        public static Result<Category> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<Category>.Failure("Name is required");
            }

            return Result<Category>.Success(new Category(name));
        }

        public void AttachCourse(Course course)
        {
            if (course != null)
            {
                if (Courses == null)
                {
                    Courses = new List<Course> { course };
                }
                else
                {
                    Courses.Add(course);
                }
            }
        }

        public void AttachDescription(string description) 
        {
            if (!string.IsNullOrWhiteSpace(description))
            {
                Description = description;
            } 
        }
    }
}
