using FluentAssertions;
using LearningManagementSystem.Domain.Entities.Courses;

namespace LearningManagementSystem.Domain.Tests
{
    public class TagTests
    {
        [Fact]
        public void When_CreateCourseIsCalled_And_ContentIsValid_Then_SuccessIsReturned()
        {
            // Arrange
            var content = "tag";

            // Act
            var result = Tag.Create(content);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void When_CreateCourseIsCalled_And_ContentIsInvalid_Then_FailureIsReturned()
        {
            // Arrange
            var content = "";
            var expectedErrorMessage = "Content is required";

            // Act
            var result = Tag.Create(content);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(expectedErrorMessage);
        }
    }
}
