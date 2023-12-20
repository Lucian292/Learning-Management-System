using FluentAssertions;
using LearningManagementSystem.Application.Features.Questions.Queries.GetQuestionById;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Queries
{
    public class GetByIdQuestionQueryTests : IDisposable
    {
        private readonly IQuestionRepository mockQuestionRepository;

        public GetByIdQuestionQueryTests()
        {
            this.mockQuestionRepository = RepositoryMocks.GetQuestionRepository();
        }
        [Fact]
        public async Task GetByIdQuestionHandler_WithValidId_ReturnsQuestion()
        {
            // Arrange
            var chapterId = new Guid("a1b2c3d4-5e6f-7a8b-9c0d-1e2f3a4b5678");
            var expectedQuestion = Question.Create("Test Question", chapterId).Value;

            mockQuestionRepository.FindByIdAsync(expectedQuestion.QuestionId).Returns(Task.FromResult(Result<Question>.Success(expectedQuestion)));

            var handler = new GetByIdQuestionQueryHandler(mockQuestionRepository);

            // Act
            var result = await handler.Handle(new GetByIdQuestionQuery(expectedQuestion.QuestionId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new QuestionDto
            {
                QuestionId = expectedQuestion.QuestionId,
                Text = expectedQuestion.Text,
                ChapterId = expectedQuestion.ChapterId,
                Choices = new()
            });
        }

        [Fact]
        public async Task GetByIdQuestionHandler_WithInvalidId_ReturnsFailure()
        {
            // Arrange
            var invalidQuestionId = new Guid("a1b2c3d4-5e6f-7a8b-9c0d-1e2f3a4b5678");

            mockQuestionRepository.FindByIdAsync(invalidQuestionId).Returns(Task.FromResult(Result<Question>.Failure("Question not found")));

            var handler = new GetByIdQuestionQueryHandler(mockQuestionRepository);

            // Act
            var result = await handler.Handle(new GetByIdQuestionQuery(invalidQuestionId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new QuestionDto());
        }

        public void Dispose()
        {
            mockQuestionRepository.ClearReceivedCalls();
        }
    }
}
