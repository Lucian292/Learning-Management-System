using FluentAssertions;
using LearningManagementSystem.Domain.Entities.Courses;

namespace LearningManagementSystem.Domain.Tests
{
    public class EnrollmentTests
    {
        [Fact]
        public void When_CreateEnrollmentIsCalled_And_ParametersAreValid_Then_SuccessIsReturned()
        {
            // Arrange
            Guid courseId = Guid.Parse("b5ac2a67-497e-48a5-9242-76cae177a1d3");
            Guid userId = Guid.Parse("5ecd6061-0360-4770-a1c0-462719c9bd04");

            // Act
            var result = Enrollment.Create(userId, courseId);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Theory]
        [InlineData("invalidGuid", "b5ac2a67-497e-48a5-9242-76cae177a1d3", "User Id is required")]
        [InlineData("b5ac2a67-497e-48a5-9242-76cae177a1d3", "invalidGuid", "Course Id is required")]
        public void When_CreateEnrollmentIsCalled_And_ParametersAreInvalid_Then_FailureIsReturned(
            string userId, string courseId, string expectedErrorMessage)
        {
            // Arrange
            Guid parsedUserId = Guid.Empty;
            Guid parsedCourseId = Guid.Empty;

            if (!string.IsNullOrWhiteSpace(courseId) && Guid.TryParse(courseId, out Guid courseGuid))
            {
                parsedCourseId = courseGuid;
            }

            if (!string.IsNullOrWhiteSpace(userId) && Guid.TryParse(userId, out Guid userGuid))
            {
                parsedUserId = userGuid;
            }

            // Act
            var result = Enrollment.Create(parsedUserId, parsedCourseId);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain(expectedErrorMessage);
        }
    }
}
