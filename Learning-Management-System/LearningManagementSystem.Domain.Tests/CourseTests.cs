using FluentAssertions;
using LearningManagementSystem.Domain.Entities.Courses;

namespace LearningManagementSystem.Domain.Tests
{
    public class CourseTests
    {
        [Fact]
        public void When_CreateCourseIsCalled_And_ParametersAreValid_Then_SuccessIsReturned()
        {
            // Arrange
            string validTitle = "ValidTitle";
            string validDescription = "ValidDescription";
            Guid validProfessorId = Guid.NewGuid();
            Guid validCategoryId = Guid.NewGuid();

            // Act
            var result = Course.Create(validTitle, validDescription, validProfessorId, validCategoryId);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Theory]
        [InlineData("", "Description", "ProfessorId", "invalidGuid", "Title is required")]
        [InlineData("Title", "", "ProfessorId", "invalidGuid", "Description is required")]
        [InlineData("Title", "Description", "invalidGuid", "invalidGuid", "Invalid Professor Id format")]
        [InlineData("Title", "Description", "b448f53d-924b-4a6c-b437-d66ce4e4cb5b", "invalidGuid", "Invalid Category Id format")]
        public void When_CreateCourseIsCalled_And_ParametersAreInvalid_Then_FailureIsReturned(
    string title, string description, string professorId, string categoryId, string expectedErrorMessage)
        {
            // Arrange
            Guid parsedProfessorId = Guid.Empty;
            Guid parsedCategoryId = Guid.Empty;

            if (!string.IsNullOrWhiteSpace(professorId) && Guid.TryParse(professorId, out Guid professorGuid))
            {
                parsedProfessorId = professorGuid;
            }

            if (!string.IsNullOrWhiteSpace(categoryId) && Guid.TryParse(categoryId, out Guid categoryGuid))
            {
                parsedCategoryId = categoryGuid;
            }

            // Act
            var result = Course.Create(title, description, parsedProfessorId, parsedCategoryId);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain(expectedErrorMessage);
        }


        [Fact]
        public void When_UpdateIsCalledAnd_ParametersAreValid_Then_CoursePropertiesAreUpdated()
        {
            // Arrange
            var course = Course.Create("InitialTitle", "InitialDescription", Guid.NewGuid(), Guid.NewGuid()).Value;
            string newTitle = "NewTitle";
            string newDescription = "NewDescription";

            // Act
            course.Update(newTitle, newDescription);

            // Assert
            course.Title.Should().Be(newTitle);
            course.Description.Should().Be(newDescription);
        }

        // Add more tests for other methods and scenarios as needed
    }
}
