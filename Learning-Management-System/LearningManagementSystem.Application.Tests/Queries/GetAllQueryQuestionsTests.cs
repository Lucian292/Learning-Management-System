using FluentAssertions;
using LearningManagementSystem.Application.Features.Questions.Queries.GetAll;
using LearningManagementSystem.Application.Persistence.Courses;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Queries
{
    public class GetAllQuestionsQueryTests : IDisposable
    {
        private readonly IQuestionRepository _mockQuestionRepository;

        public GetAllQuestionsQueryTests()
        {
            _mockQuestionRepository = RepositoryMocks.GetQuestionRepository();
        }

        [Fact]
        public async Task GetAllQuestionsHandler_Success()
        {
            // Arrange
            var handler = new GetAllQuestionQueryHandler(_mockQuestionRepository);

            // Act
            var result = await handler.Handle(new GetAllQuestionQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Questions.Should().NotBeNull();
            result.Questions.Should().HaveCount(2);
        }

        public void Dispose()
        {
            _mockQuestionRepository.ClearReceivedCalls();
        }
    }
}
