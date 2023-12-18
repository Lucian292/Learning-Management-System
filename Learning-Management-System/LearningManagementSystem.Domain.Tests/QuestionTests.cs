using FluentAssertions;
using LearningManagementSystem.Domain.Entities.Courses;

namespace LearningManagementSystem.Domain.Tests
{
    public class QuestionTests
    {
        [Fact]
        public void When_CreateQuestionIsCalled_And_ParametersAreValid_Then_SuccessIsReturned()
        {
            // Arrange
            string validText = "ValidText";
            Guid validChapterId = Guid.NewGuid();

            // Act
            var result = Question.Create(validText, validChapterId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
        }

        [Theory]
        [InlineData("", "00000000-0000-0000-0000-000000000000", "Text is required")]
        [InlineData("ValidText", "00000000-0000-0000-0000-000000000000", "Chapter Id is required")]
        public void When_CreateQuestionIsCalled_And_ParametersAreInvalid_Then_FailureIsReturned(
    string text, string chapterId, string expectedErrorMessage)
        {
            // Arrange
            var parsedChapterId = Guid.Parse(chapterId);

            // Act
            var result = Question.Create(text, parsedChapterId);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(expectedErrorMessage);
        }

        [Fact]
        public void When_CreateQuestionIsCalled_And_ParametersAreValid_Then_QuestionIsCreated()
        {
            // Arrange
            string validText = "ValidText";
            Guid validChapterId = Guid.NewGuid();

            // Act
            var result = Question.Create(validText, validChapterId);

            // Assert
            result.Value.Should().NotBeNull();
            result.Value.Text.Should().Be(validText);
            result.Value.ChapterId.Should().Be(validChapterId);
        }
    }
}
