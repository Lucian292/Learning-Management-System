using FluentAssertions;
using LearningManagementSystem.Domain.Entities.Courses;

namespace LearningManagementSystem.Domain.Tests
{
    public class RatingTests
    {
        [Fact]
        public void When_CreateRatingIsCalled_And_ParametersAreValid_Then_SuccessIsReturned()
        {
            // Arrange
            decimal value = 1.1M;

            Guid validEnrollmentId = Guid.Parse("b5ac2a67-497e-48a5-9242-76cae177a1d3");

            // Act
            var result = Rating.Create(validEnrollmentId, value);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Theory]
        [InlineData("invalidGuid", "1.0M", "Enrollment Id is required")]
        [InlineData("b5ac2a67-497e-48a5-9242-76cae177a1d3", "-1.0M", "Invalid rating value")]
        public void When_CreateRatingIsCalled_And_ParametersAreInvalid_Then_FailureIsReturned(
            string enrollmentId, string value, string expectedErrorMessage)
        {
            // Arrange
            Guid parsedEnrollmentId = Guid.Empty;
            decimal v = -1.0M;

            if (!string.IsNullOrWhiteSpace(enrollmentId) && Guid.TryParse(enrollmentId, out Guid enrollmentGuid))
            {
                parsedEnrollmentId = enrollmentGuid;
            }

            if (!string.IsNullOrWhiteSpace(value) && Decimal.TryParse(value, out decimal decValue))
            {
                v = decValue;
            }

            // Act
            var result = Rating.Create(parsedEnrollmentId, v);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain(expectedErrorMessage);
        }
    }
}
