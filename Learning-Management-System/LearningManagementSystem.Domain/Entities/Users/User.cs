using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using System.Text.RegularExpressions;


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

        private User(string firstName, string lastName, string email, string password, UserRole role = UserRole.Student)
        {
            UserId = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Role = role;
        }

        public static Result<User> Create(string firstName, string lastName, string email, string password, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Result<User>.Failure("First Name is required");
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Result<User>.Failure("Last Name is required");
            }

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, emailPattern))
            {
                return Result<User>.Failure("Invalid format for email");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                return Result<User>.Failure("Password is required");
            }

            // Verifică dacă rolul specificat este valid
            if (!Enum.IsDefined(typeof(UserRole), role))
            {
                return Result<User>.Failure("Invalid user role");
            }

            return Result<User>.Success(new User(firstName, lastName, email, password, role));
        }


        public void AttachPhoneNumber(string phoneNumber)
        {
            string phoneNumberPattern = @"^(?:\+40|0)[0-9]{9}$";
            if (!string.IsNullOrWhiteSpace(phoneNumber) && Regex.IsMatch(phoneNumber, phoneNumberPattern))
            {
                PhoneNumber = phoneNumber;
            }
        }
    }   

}
