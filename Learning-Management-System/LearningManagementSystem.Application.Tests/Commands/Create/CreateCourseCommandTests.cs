using FluentAssertions;
using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Features.Couerses.Commands.CreateCourse;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Features.Courses.Commands
{
    public class CreateCourseCommandHandlerTests : IDisposable
    {
        private readonly ICourseRepository mockRepository;
        private readonly ICurrentUserService mockUserService;

        public CreateCourseCommandHandlerTests()
        {
            this.mockRepository = RepositoryMocks.GetCourseRepository();
            this.mockUserService = Substitute.For<ICurrentUserService>();
        }

        [Fact]
        public async Task CreateCourseCommandHandle_ValidCommand_ReturnsSuccessResponse()
        {
            // Arrange
            var title = "Test Course";
            var description = "Test Description";
            var categoryId = Guid.NewGuid();

            var userId = Guid.NewGuid();
            mockUserService.UserId.Returns(userId.ToString());

            var command = new CreateCourseCommand
            {
                Title = title,
                Description = description,
                CategoryId = categoryId
            };

            var handler = new CreateCourseCommandHandler(mockRepository, mockUserService);
            var course = Course.Create(title, description, userId, categoryId).Value;


            // Assume that Course.Create method returns a Result<Course> with a new course
            var expectedResult = Result<Course>.Success(course);

            mockRepository.AddAsync(Arg.Any<Course>()).Returns(expectedResult);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.ValidationsErrors.Should().BeNull();
            result.Course.Should().NotBeNull();
            result.Course.CourseId.Should().NotBeEmpty();
            result.Course.Title.Should().Be(title);
            result.Course.Description.Should().Be(description);
            result.Course.UserId.Should().Be(userId);
            result.Course.CategoryId.Should().Be(categoryId);
        }

        [Fact]
        public async Task CreateCourseCommandHandle_InvalidCommand_ReturnsFailureResponse()
        {
            // Arrange
            var invalidTitle = ""; // Invalid title
            var invalidDescription = "Test Description";
            var categoryId = Guid.NewGuid();

            var userId = Guid.NewGuid();
            mockUserService.UserId.Returns(userId.ToString());

            var command = new CreateCourseCommand
            {
                Title = invalidTitle,
                Description = invalidDescription,
                CategoryId = categoryId
            };

            var handler = new CreateCourseCommandHandler(mockRepository, mockUserService);

            // Assume that Course.Create method returns a failure result
            var expectedErrorMessage = "Title is required.";
            var expectedResult = Result<Course>.Failure(expectedErrorMessage);

            mockRepository.AddAsync(Arg.Any<Course>()).Returns(expectedResult);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.ValidationsErrors.Should().NotBeNull().And.NotBeEmpty();
            result.ValidationsErrors.Should().BeEquivalentTo([ expectedErrorMessage ]);
            result.Course.Should().BeNull();
        }

        public void Dispose()
        {
            mockRepository.ClearReceivedCalls();
        }
    }
}
