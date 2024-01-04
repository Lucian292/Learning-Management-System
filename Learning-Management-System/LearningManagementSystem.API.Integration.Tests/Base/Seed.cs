using LearningManagementSystem.Domain.Entities;
using LearningManagementSystem.Infrastructure.Data;

namespace LearningManagementSystem.API.Integration.Tests.Base
{
    public class Seed
    {
        public static void InitializeDbForTests(LearningManagementSystemDbContext context)
        {
            var categories = new List<Category>
            {
                Category.Create("Informatica").Value,
                Category.Create("Biologie").Value,
                Category.Create("Chimie").Value,
                Category.Create("Muzica").Value
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
    }
}
