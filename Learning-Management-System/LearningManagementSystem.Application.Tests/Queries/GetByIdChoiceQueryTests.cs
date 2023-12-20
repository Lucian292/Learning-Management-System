using FluentAssertions;
using LearningManagementSystem.Application.Features.Choice.Queries;
using LearningManagementSystem.Application.Features.Choice.Queries.GetById;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Queries
{ 
    public class GetByIdChoiceQueryTests : IDisposable
    {
        private readonly IChoiceRepository mockChoiceRepository;

        public GetByIdChoiceQueryTests()
        {
            this.mockChoiceRepository = RepositoryMocks.GetChoiceRepository();
        }
        [Fact]
        public async Task GetByIdChoiceHandler_WithValidId_ReturnsChoice()
        {
            // Arrange
            var questionId = new Guid("a1b2c3d4-5e6f-7a8b-9c0d-1e2f3a4b5678");
            var expectedChoice = Choice.Create("Test Choice", questionId, true).Value;

            mockChoiceRepository.FindByIdAsync(expectedChoice.ChoiceId).Returns(Task.FromResult(Result<Choice>.Success(expectedChoice)));

            var handler = new GetByIdChoiceQueryHandler(mockChoiceRepository);

            // Act
            var result = await handler.Handle(new GetByIdChoiceQuery(expectedChoice.ChoiceId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new ChoiceDto
            {
                ChoiceId = expectedChoice.ChoiceId,
                Content = expectedChoice.Content,
                QuestionId = expectedChoice.QuestionId,
                IsCorrect = expectedChoice.IsCorrect
            });
        }

        [Fact]
        public async Task GetByIdChoiceHandler_WithInvalidId_ReturnsFailure()
        {
            // Arrange
            var invalidChoiceId = new Guid("a1b2c3d4-5e6f-7a8b-9c0d-1e2f3a4b5678");

            mockChoiceRepository.FindByIdAsync(invalidChoiceId).Returns(Task.FromResult(Result<Choice>.Failure("Choice not found")));

            var handler = new GetByIdChoiceQueryHandler(mockChoiceRepository);

            // Act
            var result = await handler.Handle(new GetByIdChoiceQuery(invalidChoiceId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new ChoiceDto());
        }

        public void Dispose()
        {
            mockChoiceRepository.ClearReceivedCalls();
        }
    }
}
