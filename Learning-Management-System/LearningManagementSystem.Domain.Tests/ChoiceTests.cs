using FluentAssertions;
using LearningManagementSystem.Domain.Entities.Courses;

namespace LearningManagementSystem.Domain.Tests
{
    public class ChoiceTests
    {
        [Fact]
        public void When_CreateChoiceIsCalled_And_ParametersAreValid_Then_SuccessIsReturned()
        {
            // Arrange
            string validContent = "ValidContent";
            Guid validQuestionId = Guid.NewGuid();
            bool validIsCorrect = true;

            // Act
            var result = Choice.Create(validContent, validQuestionId, validIsCorrect);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
        }

        [Theory]
        [InlineData("", default, true, "Content is required")]
        [InlineData("ValidContent", default, true, "Question Id can't be null")]
        public void When_CreateChoiceIsCalled_And_ParametersAreInvalid_Then_FailureIsReturned(
            string content, Guid questionId, bool isCorrect, string expectedErrorMessage)
        {
            // Arrange
            // Act
            var result = Choice.Create(content, questionId, isCorrect);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(expectedErrorMessage);
        }

        [Fact]
        public void When_CreateChoiceIsCalled_And_ParametersAreValid_Then_ChoiceIsCreated()
        {
            // Arrange
            string validContent = "ValidContent";
            Guid validQuestionId = Guid.NewGuid();
            bool validIsCorrect = true;

            // Act
            var result = Choice.Create(validContent, validQuestionId, validIsCorrect);

            // Assert
            result.Value.Should().NotBeNull();
            result.Value.Content.Should().Be(validContent);
            result.Value.QuestionId.Should().Be(validQuestionId);
            result.Value.IsCorrect.Should().Be(validIsCorrect);
        }
    }
}
