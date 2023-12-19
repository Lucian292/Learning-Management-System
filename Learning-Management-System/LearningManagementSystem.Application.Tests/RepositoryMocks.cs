using LearningManagementSystem.Application.Persistence;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities;
using LearningManagementSystem.Domain.Entities.Courses;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests
{
    public class RepositoryMocks
    {
        public static ICategoryRepository GetCategoryRepository()
        {
            var mockRepository = NSubstitute.Substitute.For<ICategoryRepository>();

            // Define your predefined categories
            var predefinedCategories = new List<Category>
            {
               Category.Create("Category1").Value,
               Category.Create("Category2").Value,
            };

            // Set up the mock repository to return predefined categories
            mockRepository.GetAllAsync().Returns(Task.FromResult(Result<IReadOnlyList<Category>>.Success(predefinedCategories)));

            return mockRepository;
        }

        public static ICourseRepository GetCourseRepository()
        {
            var mockRepository = NSubstitute.Substitute.For<ICourseRepository>();

            // Define your predefined courses
            var category = Category.Create("Category1").Value;
            var professorId = new Guid("44444444-4444-4444-4444-444444444444");

            var predefinedCourses = new List<Course>
            {
                Course.Create("Course1 Title", "Course1 Description", professorId, category.CategoryId).Value,
                Course.Create("Course2 Title", "Course2 Description", professorId, category.CategoryId).Value,
            };

            predefinedCourses[0].Category = category;
            predefinedCourses[1].Category = category;

            // Set up the mock repository to return predefined courses
            mockRepository.GetAllAsync().Returns(Task.FromResult(Result<IReadOnlyList<Course>>.Success(predefinedCourses)));

            return mockRepository;
        }

        public static ITagRepository GetTagRepository()
        {
            var mockRepository = NSubstitute.Substitute.For<ITagRepository>();

            // Define your predefined tags
            var predefinedTags = new List<Tag>
            {
                Tag.Create("Tag1").Value,
                Tag.Create("Tag2").Value,
            };

            // Set up the mock repository to return predefined tags
            mockRepository.GetAllAsync().Returns(Task.FromResult(Result<IReadOnlyList<Tag>>.Success(predefinedTags)));

            return mockRepository;
        }

        public static IEnrollmentRepository GetEnrollmentRepository()
        {
            var mockRepository = NSubstitute.Substitute.For<IEnrollmentRepository>();

            var userId1 = new Guid("c78a51a7-01fc-42bd-8c5f-5f5c9c9439a1");
            var userId2 = new Guid("3f1e566c-ef9e-4da1-a6d5-90fe1f6c1b25");
            var courseId1 = new Guid("b9d6f59c-7f74-4a8b-943f-dc7e8b2f0a33");
            var courseId2 = new Guid("8e208307-c76e-4b1f-b1d1-d1f33c44e78e");

            // Define your predefined enrollments
            var predefinedEnrollments = new List<Enrollment>
            {
                Enrollment.Create(userId1, courseId1).Value,
                Enrollment.Create(userId2, courseId2).Value,
            };

            // Set up the mock repository to return predefined enrollments
            mockRepository.GetAllAsync().Returns(Task.FromResult(Result<IReadOnlyList<Enrollment>>.Success(predefinedEnrollments)));

            return mockRepository;
        }

        public static IChapterRepository GetChapterRepository()
        {
            var mockRepository = NSubstitute.Substitute.For<IChapterRepository>();

            // Define your predefined chapters
            var course = Course.Create("Test Course", "Test Course Description", new Guid("c78a51a7-01fc-42bd-8c5f-5f5c9c9439a1"), new Guid("8e208307-c76e-4b1f-b1d1-d1f33c44e78e")).Value;

            var predefinedChapters = new List<Chapter>
            {
                Chapter.Create(course.CourseId, "Chapter1").Value,
                Chapter.Create(course.CourseId, "Chapter2").Value,
            };

            predefinedChapters[0].Course = course;
            predefinedChapters[1].Course = course;

            // Set up the mock repository to return predefined chapters
            mockRepository.GetAllAsync().Returns(Task.FromResult(Result<IReadOnlyList<Chapter>>.Success(predefinedChapters)));

            return mockRepository;
        }

        public static IQuestionRepository GetQuestionRepository()
        {
            var mockRepository = NSubstitute.Substitute.For<IQuestionRepository>();

            // Define your predefined questions
            var chapter = Chapter.Create(new Guid("b9d6f59c-7f74-4a8b-943f-dc7e8b2f0a33"), "Chapter Title").Value;

            var predefinedQuestions = new List<Question>
            {
                Question.Create("Question1 Text", chapter.ChapterId).Value,
                Question.Create("Question2 Text", chapter.ChapterId).Value,
            };

            predefinedQuestions[0].Chapter = chapter;
            predefinedQuestions[1].Chapter = chapter;

            // Set up the mock repository to return predefined questions
            mockRepository.GetAllAsync().Returns(Task.FromResult(Result<IReadOnlyList<Question>>.Success(predefinedQuestions)));

            return mockRepository;
        }

        public static IChoiceRepository GetChoiceRepository()
        {
            var mockRepository = NSubstitute.Substitute.For<IChoiceRepository>();

            // Define your predefined choices
            var question = Question.Create("Question Test", new Guid("c78a51a7-01fc-42bd-8c5f-5f5c9c9439a1")).Value;

            var predefinedChoices = new List<Choice>
            {
                Choice.Create("Choice1 Content", question.QuestionId, true).Value,
                Choice.Create("Choice2 Content", question.QuestionId, false).Value,
            };

            // Set up the mock repository to return predefined choices
            mockRepository.GetAllAsync().Returns(Task.FromResult(Result<IReadOnlyList<Choice>>.Success(predefinedChoices)));

            return mockRepository;
        }
    }
}
