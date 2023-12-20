using FluentAssertions;
using LearningManagementSystem.Application.Features.Tags.Queries;
using LearningManagementSystem.Application.Features.Tags.Queries.GetById;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Queries
{
    public class GetByIdTagQueryTests : IDisposable
    {
        private readonly ITagRepository mockTagRepository;

        public GetByIdTagQueryTests()
        {
            this.mockTagRepository = RepositoryMocks.GetTagRepository();
        }
        [Fact]
        public async Task GetByIdTagHandler_WithValidId_ReturnsTag()
        {
            // Arrange
            var expectedTag = Tag.Create("Test Tag").Value;

            mockTagRepository.FindByIdAsync(expectedTag.TagId).Returns(Task.FromResult(Result<Tag>.Success(expectedTag)));

            var handler = new GetByIdTagQueryHandler(mockTagRepository);

            // Act
            var result = await handler.Handle(new GetByIdTagQuery { TagId = expectedTag.TagId }, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new TagDto
            {
                TagId = expectedTag.TagId,
                Content = expectedTag.Content
            });
        }

        [Fact]
        public async Task GetByIdTagHandler_WithInvalidId_ReturnsFailure()
        {
            // Arrange
            var invalidTagId = new Guid("a1b2c3d4-5e6f-7a8b-9c0d-1e2f3a4b5678");

            mockTagRepository.FindByIdAsync(invalidTagId).Returns(Task.FromResult(Result<Tag>.Failure("Tag not found")));

            var handler = new GetByIdTagQueryHandler(mockTagRepository);

            // Act
            var result = await handler.Handle(new GetByIdTagQuery { TagId = invalidTagId }, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new TagDto());
        }

        public void Dispose()
        {
            mockTagRepository.ClearReceivedCalls();
        }
    }
}
