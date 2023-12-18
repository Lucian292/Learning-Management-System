using FluentAssertions;
using LearningManagementSystem.Domain.Entities.Courses;

namespace LearningManagementSystem.Domain.Tests
{
    public class QuestionResultTests
    {
        [Fact]
        public void When_CreateQuestionResultIsCalled_And_ParametersAreValid_Then_SuccessIsReturned()
        {
            // Arrange
            Guid validQuestionId = Guid.NewGuid();
            Guid validUserId = Guid.NewGuid();
            bool validIsCorrect = true;

            // Act
            var result = QuestionResult.Create(validQuestionId, validUserId, validIsCorrect);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
        }

        [Fact]
        public void When_CreateQuestionResultIsCalled_And_QuestionIdIsInvalid_Then_FailureIsReturned()
        {
            // Arrange
            var questionId = "00000000-0000-0000-0000-000000000000";
            var userId = "validUserId";
            var isCorrect = true;
            var expectedErrorMessage = "Question Id is required";

            Guid parsedQuestionId = Guid.TryParse(questionId, out Guid questionGuid) ? questionGuid : Guid.Empty;

            // Act
            var result = QuestionResult.Create(parsedQuestionId, Guid.NewGuid(), isCorrect);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(expectedErrorMessage);
        }

        [Fact]
        public void When_CreateQuestionResultIsCalled_And_UserIdIsInvalid_Then_FailureIsReturned()
        {
            // Arrange
            var questionId = "validQuestionId";
            var userId = "00000000-0000-0000-0000-000000000000";
            var isCorrect = true;
            var expectedErrorMessage = "User Id is required";

            Guid parsedUserId = Guid.TryParse(userId, out Guid userGuid) ? userGuid : Guid.Empty;

            // Act
            var result = QuestionResult.Create(Guid.NewGuid(), parsedUserId, isCorrect);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(expectedErrorMessage);
        }



        [Fact]
        public void When_CreateQuestionResultIsCalled_And_ParametersAreValid_Then_QuestionResultIsCreated()
        {
            // Arrange
            Guid validQuestionId = Guid.NewGuid();
            Guid validUserId = Guid.NewGuid();
            bool validIsCorrect = true;

            // Act
            var result = QuestionResult.Create(validQuestionId, validUserId, validIsCorrect);

            // Assert
            result.Value.Should().NotBeNull();
            result.Value.QuestionId.Should().Be(validQuestionId);
            result.Value.UserId.Should().Be(validUserId);
            result.Value.IsCorrect.Should().Be(validIsCorrect);
        }
    }
}
