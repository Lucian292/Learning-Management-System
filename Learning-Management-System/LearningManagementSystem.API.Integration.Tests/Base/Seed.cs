using LearningManagementSystem.Domain.Entities;
using LearningManagementSystem.Domain.Entities.Courses;
using LearningManagementSystem.Infrastructure.Data;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LearningManagementSystem.API.Integration.Tests.Base
{
    public class Seed
    {
        public static void InitializeDbForTests(LearningManagementSystemDbContext context)
        {
            Guid userId = Guid.Parse("a0f5a0f5-0f5a-0f5a-0f5a-0f5a0f5a0f5a");

            var categories = new List<Category>
            {
                Category.Create("Informatica").Value,
                Category.Create("Biologie").Value,
                Category.Create("Chimie").Value,
                Category.Create("Muzica").Value
            };
            Guid categoryGuid = categories[0].CategoryId;
            var courses = new List<Course>
            {
                Course.Create("Curs Informatica", "Curs de informatica", userId, categoryGuid).Value,
                Course.Create("Curs Biologie", "Curs de biologie", userId, categoryGuid).Value,
                Course.Create("Curs Chimie", "Curs de chimie", userId, categoryGuid).Value,
                Course.Create("Curs Muzica", "Curs de muzica", userId, categoryGuid).Value
            };
            Guid courseGuid = courses[0].CourseId;
            var chapters = new List<Chapter>
            {
                Chapter.Create(courseGuid, "Titlu 1").Value,
                Chapter.Create(courseGuid, "Titlu 2").Value,
                Chapter.Create(courseGuid, "Titlu 3").Value,
                Chapter.Create(courseGuid, "Titlu 4").Value
            };
            Guid chapterGuid = chapters[0].ChapterId;
            context.Categories.AddRange(categories);
            context.Courses.AddRange(courses);
            context.SaveChanges();
        }
    }
}
