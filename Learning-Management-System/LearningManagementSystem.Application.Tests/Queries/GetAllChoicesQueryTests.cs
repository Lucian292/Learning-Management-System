using FluentAssertions;
using LearningManagementSystem.Application.Features.Choice.Queries.GetAll;
using LearningManagementSystem.Application.Persistence.Courses;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Queries
{
    public class GetAllChoicesQueryTests : IDisposable
    {
        private readonly IChoiceRepository _mockChoiceRepository;

        public GetAllChoicesQueryTests()
        {
            _mockChoiceRepository = RepositoryMocks.GetChoiceRepository();
        }

        [Fact]
        public async Task GetAllChoicesHandler_Success()
        {
            // Arrange
            var handler = new GetAllChoicesQueryHandler(_mockChoiceRepository);

            // Act
            var result = await handler.Handle(new GetAllChoicesQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Choices.Should().NotBeNull();
            result.Choices.Should().HaveCount(2);

        }

        public void Dispose()
        {
            _mockChoiceRepository.ClearReceivedCalls();
        }
    }
}
