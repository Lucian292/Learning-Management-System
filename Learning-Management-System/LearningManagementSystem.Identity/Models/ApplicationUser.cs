using Microsoft.AspNetCore.Identity;

namespace LearningManagementSystem.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? LastName { get; set; }

        public string? FirstName { get; set; }
    }
}
