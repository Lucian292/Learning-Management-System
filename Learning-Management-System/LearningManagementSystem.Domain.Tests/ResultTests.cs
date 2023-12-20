using FluentAssertions;
using LearningManagementSystem.Domain.Common;
using Xunit;

namespace LearningManagementSystem.Domain.Tests.Common
{
    public class ResultTests
    {
        [Fact]
        public void When_CreatingSuccessResult_Then_SuccessIsTrueAndValueIsSet()
        {
            // Arrange
            var expectedValue = new object();

            // Act
            var result = Result<object>.Success(expectedValue);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(expectedValue);
            result.Error.Should().BeNull();
        }

        [Fact]
        public void When_CreatingFailureResult_Then_SuccessIsFalseAndErrorIsSet()
        {
            // Arrange
            var expectedError = "An error occurred.";

            // Act
            var result = Result<object>.Failure(expectedError);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Error.Should().Be(expectedError);
        }

        [Fact]
        public void When_CreatingFailureResultWithNullError_Then_SuccessIsFalseAndErrorIsNull()
        {
            // Arrange

            // Act
            var result = Result<object>.Failure(null);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Error.Should().BeNull();
        }
    }
}
