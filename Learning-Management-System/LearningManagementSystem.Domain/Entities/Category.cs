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
    }
}
