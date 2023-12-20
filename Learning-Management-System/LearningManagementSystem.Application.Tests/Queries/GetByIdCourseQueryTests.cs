using FluentAssertions;
using LearningManagementSystem.Application.Features.Courses.Queries;
using LearningManagementSystem.Application.Features.Courses.Queries.GetById;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities;
using LearningManagementSystem.Domain.Entities.Courses;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Queries
{
    public class GetByIdCourseQueryTests : IDisposable
    {
        private readonly ICourseRepository mockCourseRepository;

        public GetByIdCourseQueryTests()
        {
            this.mockCourseRepository = RepositoryMocks.GetCourseRepository();
        }
        [Fact]
        public async Task GetByIdCourseHandler_WithValidId_ReturnsCourse()
        {
            // Arrange
            var category = Category.Create("Test Category").Value;
            var professorId = new Guid("c14a5e67-89a4-48e2-8a34-2c287506a2e5");
            var expectedCourse = Course.Create("Test Course", "Test Description", professorId, category.CategoryId).Value;
            expectedCourse.Category = category;
            
            var courseId = expectedCourse.CourseId;

            mockCourseRepository.FindByIdAsync(courseId).Returns(Task.FromResult(Result<Course>.Success(expectedCourse)));

            var handler = new GetByIdCourseQueryHandler(mockCourseRepository);

            // Act
            var result = await handler.Handle(new GetByIdCourseQuery(courseId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new CourseDto
            {
                CourseId = expectedCourse.CourseId,
                Title = expectedCourse.Title,
                Description = expectedCourse.Description,
                UserId = expectedCourse.ProfessorId,
                CategoryId = expectedCourse.CategoryId,
                Chapters = new()
            }); 
        }

        [Fact]
        public async Task GetByIdCourseHandler_WithInvalidId_ReturnsFailure()
        {
            // Arrange
            var invalidCourseId = new Guid("a1b2c3d4-5e6f-7a8b-9c0d-1e2f3a4b5678");

            mockCourseRepository.FindByIdAsync(invalidCourseId).Returns(Task.FromResult(Result<Course>.Failure("Course not found")));

            var handler = new GetByIdCourseQueryHandler(mockCourseRepository);

            // Act
            var result = await handler.Handle(new GetByIdCourseQuery(invalidCourseId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new CourseDto());
        }

        public void Dispose()
        {
            mockCourseRepository.ClearReceivedCalls();
        }
    }
}

