using FluentAssertions;
using LearningManagementSystem.Application.Features.Chapters.Queries.GetAll;
using LearningManagementSystem.Application.Persistence.Courses;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Queries
{
    public class GetAllChaptersQueryTests : IDisposable
    {
        private readonly IChapterRepository _mockChapterRepository;

        public GetAllChaptersQueryTests()
        {
            _mockChapterRepository = RepositoryMocks.GetChapterRepository();
        }

        [Fact]
        public async Task GetAllChaptersHandler_Success()
        {
            // Arrange
            var handler = new GetAllChaptersQueryHandler(_mockChapterRepository);

            // Act
            var result = await handler.Handle(new GetAllChaptersQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Chapters.Should().NotBeNull();
            result.Chapters.Should().HaveCount(2);
        }

        public void Dispose()
        {
            _mockChapterRepository.ClearReceivedCalls();
        }
    }
}
