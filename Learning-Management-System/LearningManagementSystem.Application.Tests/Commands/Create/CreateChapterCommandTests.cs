using FluentAssertions;
using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Features.Chapters.Commands.CreateChapter;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Features.Chapters.Commands
{
    public class CreateChapterCommandTests : IDisposable
    {
        private readonly IChapterRepository mockChapterRepository;
        private readonly ICurrentUserService mockUserService;
        private readonly ICourseRepository mockCourseRepository;

        public CreateChapterCommandTests()
        {
            this.mockChapterRepository = RepositoryMocks.GetChapterRepository();
            this.mockUserService = Substitute.For<ICurrentUserService>();
            this.mockCourseRepository = RepositoryMocks.GetCourseRepository();
        }

        [Fact]
        public async Task CreateChapterCommandHandle_ValidCommand_ReturnsSuccessResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            mockUserService.UserId.Returns(userId.ToString());
            mockUserService.IsUserAdmin().Returns(false);

            var course = Course.Create("T", "D", userId, Guid.NewGuid()).Value;
            var courseId = course.CourseId;
            var title = "Test Chapter";
            var link = "Test Link";
            var content = new byte[] { 1, 2, 3 };

            var command = new CreateChapterCommand
            {
                CourseId = courseId,
                Title = title,
                Link = link,
                Content = content
            };

            var handler = new CreateChapterCommandHandler(mockChapterRepository, mockUserService, mockCourseRepository);

            var chapter = Chapter.Create(courseId, title).Value;
            chapter.AttachLink(link);
            chapter.AttachContent(content);
            chapter.Course = course;

            mockCourseRepository.FindByIdAsync(courseId).Returns(Result<Course>.Success(course));
            mockCourseRepository.IsCourseOwnedByUserAsync(courseId, userId).Returns(true);

            // Assume that Chapter.Create method returns a Result<Chapter> with a new chapter
            var expectedResult = Result<Chapter>.Success(chapter);

            mockChapterRepository.AddAsync(Arg.Any<Chapter>()).Returns(expectedResult);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.ValidationsErrors.Should().BeNull();
            result.Chapter.Should().NotBeNull();
            result.Chapter.ChapterId.Should().NotBeEmpty();
            result.Chapter.CourseId.Should().Be(courseId);
            result.Chapter.Title.Should().Be(title);
            result.Chapter.Link.Should().Be(link);
            Assert.Equal(result.Chapter.Content, content);
        }

        [Fact]
        public async Task CreateChapterCommandHandle_InvalidCommand_ReturnsFailureResponse()
        {
            // Arrange
            var invalidCourseId = Guid.Empty;
            var invalidTitle = "Title";
            var link = "Test Link";
            var content = new byte[] { 1, 2, 3 };

            var userId = Guid.NewGuid();
            mockUserService.UserId.Returns(userId.ToString());
            mockUserService.IsUserAdmin().Returns(false);

            var command = new CreateChapterCommand
            {
                CourseId = invalidCourseId,
                Title = invalidTitle,
                Link = link,
                Content = content
            };

            var handler = new CreateChapterCommandHandler(mockChapterRepository, mockUserService, mockCourseRepository);

            mockCourseRepository.IsCourseOwnedByUserAsync(invalidCourseId, userId).Returns(true);

            // Assume that Chapter.Create method returns a failure result
            var expectedErrorMessage = "Course Id must not be empty.";
            // var expectedResult = Result<Chapter>.Failure(expectedErrorMessage);

            // mockChapterRepository.AddAsync(Arg.Any<Chapter>()).Returns(expectedResult);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.ValidationsErrors.Should().NotBeNull().And.NotBeEmpty();
            result.ValidationsErrors.Should().Contain(expectedErrorMessage);
            result.Chapter.Should().BeNull();
        }

        public void Dispose()
        {
            mockChapterRepository.ClearReceivedCalls();
            mockCourseRepository.ClearReceivedCalls();
            mockUserService.ClearReceivedCalls();
        }
    }
}
