using FluentAssertions;
using LearningManagementSystem.Application.Features.Tags.Queries.GetAll;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Queries
{
    public class GetAllTagsQueryTests : IDisposable
    {
        private readonly ITagRepository _mockTagRepository;

        public GetAllTagsQueryTests()
        {
            _mockTagRepository = RepositoryMocks.GetTagRepository();
        }

        [Fact]
        public async Task GetAllTagsHandler_Success()
        {
            // Arrange
            var handler = new GetAllTagQueryHandler(_mockTagRepository);

            // Act
            var result = await handler.Handle(new GetAllTagQuery(),CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Tags.Should().NotBeNull();
            result.Tags.Should().HaveCount(2);
        }

        public void Dispose()
        {
            _mockTagRepository.ClearReceivedCalls();
        }
    }
}
