using LearningManagementSystem.Domain.Common;

namespace LearningManagementSystem.Application.Features.Users.Queries
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; }= default!;
        public UserRole Role { get; set; }= default!;
    }
}
