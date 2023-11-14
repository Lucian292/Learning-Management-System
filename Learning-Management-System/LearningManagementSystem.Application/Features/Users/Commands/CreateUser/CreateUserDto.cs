using LearningManagementSystem.Domain.Common;

namespace LearningManagementSystem.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserDto
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserRole Role { get; set; }

    }
}
