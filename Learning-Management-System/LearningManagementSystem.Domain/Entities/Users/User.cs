using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Domain.Entities.Users
{
    public class User : AuditableEntity
    {
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string? PhoneNumber {  get; private set; }
        public UserRole Role { get; private set; }

        public List<Enrollment>? EnrolledCourses { get; private set; }

        private User(string firstName, string lastName, string email, string password, string phoneNumber, UserRole role, List<Enrollment> courses) 
        {
            UserId = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;   
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            Role = role;
            EnrolledCourses = courses;
        }
    }
}
