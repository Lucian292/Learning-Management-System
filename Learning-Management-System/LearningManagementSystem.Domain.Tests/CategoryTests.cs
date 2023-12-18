using FluentAssertions;
using LearningManagementSystem.Domain.Entities;
using LearningManagementSystem.Domain.Entities.Courses;

namespace LearningManagementSystem.Domain.Tests
{
    public class CategoryTests
    {
        [Fact]
        public void When_CreateCategoryIsCalled_And_CategoryNameIsValid_Then_SuccessIsReturned()
        {
            // Arrange
            string validCategoryName = "ValidCategory";

            // Act
            var result = Category.Create(validCategoryName);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void When_CreateCategoryIsCalled_And_CategoryNameIsNull_Then_FailureIsReturned()
        {
            // Arrange
            string invalidCategoryName = string.Empty;

            // Act
            var result = Category.Create(invalidCategoryName);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Name is required");
        }

        [Fact]
        public void When_AttachCourseIsCalled_And_CourseIsNotNull_Then_CourseIsAttachedToCategory()
        {
            // Arrange
            var category = Category.Create("TestCategory").Value;
            var course = Course.Create("TestCourse", "CourseDescription", Guid.NewGuid(), Guid.NewGuid()).Value;

            // Act
            category.AttachCourse(course);

            // Assert
            category.Courses.Should().Contain(course);
        }

        [Fact]
        public void When_AttachCourseIsCalled_And_CourseIsNull_Then_CategoryCoursesRemainUnchanged()
        {
            // Arrange
            var category = Category.Create("TestCategory").Value;
            var initialCourses = category.Courses;

            // Act
            category.AttachCourse(null);

            // Assert
            category.Courses.Should().BeEquivalentTo(initialCourses);
        }

        [Fact]
        public void When_AttachDescriptionIsCalled_And_DescriptionIsNotEmpty_Then_DescriptionIsAttachedToCategory()
        {
            // Arrange
            var category = Category.Create("TestCategory").Value;
            string description = "CategoryDescription";

            // Act
            category.AttachDescription(description);

            // Assert
            category.Description.Should().Be(description);
        }

        [Fact]
        public void When_AttachDescriptionIsCalled_And_DescriptionIsEmpty_Then_CategoryDescriptionRemainsUnchanged()
        {
            // Arrange
            var category = Category.Create("TestCategory").Value;
            var initialDescription = category.Description;

            // Act
            category.AttachDescription(string.Empty);

            // Assert
            category.Description.Should().Be(initialDescription);
        }

        [Fact]
        public void When_UpdateCategoryIsCalled_Then_CategoryPropertiesAreUpdated()
        {
            // Arrange
            var category = Category.Create("TestCategory").Value;
            string newCategoryName = "NewTestCategory";
            string newDescription = "NewCategoryDescription";

            // Act
            category.UpdateCategory(newCategoryName, newDescription);

            // Assert
            category.CategoryName.Should().Be(newCategoryName);
            category.Description.Should().Be(newDescription);
        }
    }
}
