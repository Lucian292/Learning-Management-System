using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;

namespace LearningManagementSystem.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public Guid CategoryId { get; private set; }
        public string? Description { get; private set; }
        public List<Course> Courses { get; private set; } = new();
        public string CategoryName { get; private set; }

        private Category(string categoryName)
        {
            CategoryId = Guid.NewGuid();
            CategoryName = categoryName;
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

        public Result<Category> UpdateCategory(string categoryName, string? description)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return Result<Category>.Failure("CategoryName cannot be null or empty.");
            }

            CategoryName = categoryName;
            Description = description;

            return Result<Category>.Success(this);
        }
    }
}
