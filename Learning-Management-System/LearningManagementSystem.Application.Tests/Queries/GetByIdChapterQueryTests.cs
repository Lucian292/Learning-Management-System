using FluentAssertions;
using LearningManagementSystem.Application.Features.Chapters.Queries;
using LearningManagementSystem.Application.Features.Chapters.Queries.GetById;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Queries
{
    public class GetByIdChapterQueryTests : IDisposable
    {
        private readonly IChapterRepository mockChapterRepository;

        public GetByIdChapterQueryTests()
        {
            this.mockChapterRepository = RepositoryMocks.GetChapterRepository();
        }
        [Fact]
        public async Task GetByIdChapterHandler_WithValidId_ReturnsChapter()
        {
            // Arrange
            var courseId = new Guid("a1b2c3d4-5e6f-7a8b-9c0d-1e2f3a4b5678");
            var expectedChapter = Chapter.Create(courseId, "Test Chapter").Value;

            mockChapterRepository.FindByIdAsync(expectedChapter.ChapterId).Returns(Task.FromResult(Result<Chapter>.Success(expectedChapter)));

            var handler = new GetByIdChapterHandler(mockChapterRepository);

            // Act
            var result = await handler.Handle(new GetByIdChapterQuery(expectedChapter.ChapterId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new ChapterDto
            {
                ChapterId = expectedChapter.ChapterId,
                Title = expectedChapter.Title,
                CourseId = expectedChapter.CourseId,
                Content = expectedChapter.Content,
                Questions = new()
            });
        }

        [Fact]
        public async Task GetByIdChapterHandler_WithInvalidId_ReturnsFailure()
        {
            // Arrange
            var invalidChapterId = new Guid("a1b2c3d4-5e6f-7a8b-9c0d-1e2f3a4b5678");

            mockChapterRepository.FindByIdAsync(invalidChapterId).Returns(Task.FromResult(Result<Chapter>.Failure("Chapter not found")));

            var handler = new GetByIdChapterHandler(mockChapterRepository);

            // Act
            var result = await handler.Handle(new GetByIdChapterQuery(invalidChapterId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new ChapterDto());
        }

        public void Dispose()
        {
            mockChapterRepository.ClearReceivedCalls();
        }
    }
}
