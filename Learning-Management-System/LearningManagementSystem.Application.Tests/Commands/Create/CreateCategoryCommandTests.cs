using FluentAssertions;
using LearningManagementSystem.Application.Features.Categories.Commands.CreateCategory;
using LearningManagementSystem.Application.Persistence;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Features.Categories.Commands
{
    public class CreateCategoryCommandTests : IDisposable
    {
        private readonly ICategoryRepository mockRepository;

        public CreateCategoryCommandTests()
        {
            this.mockRepository = RepositoryMocks.GetCategoryRepository();
        }
        [Fact]
        public async Task CreateCategoryCommandHandle_ValidCommand_ReturnsSuccessResponse()
        {
            // Arrange
            var categoryName = "Test Category One";
            var description = "Test Description";

            var category = Category.Create(categoryName).Value;
            category.AttachDescription(description);

            var command = new CreateCategoryCommand
            {
                CategoryName = categoryName,
                Description = description
            };

            var handler = new CreateCategoryCommandHandler(mockRepository);

            // Assume that Category.Create method returns a Result<Category> with a new category
            var expectedResult = Result<Category>.Success(category);

            mockRepository.AddAsync(Arg.Any<Category>()).Returns(expectedResult);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.ValidationsErrors.Should().BeNull();
            result.Category.Should().NotBeNull();
            result.Category.CategoryName.Should().Be(categoryName);
            result.Category.Description.Should().Be(description);
        }

        [Fact]
        public async Task CreateCategoryCommandHandle_InvalidCommand_ReturnsFailureResponse()
        {
            // Arrange
            var invalidCategoryName = ""; // Invalid category name
            var invalidDescription = "Test Description";

            var command = new CreateCategoryCommand
            {
                CategoryName = invalidCategoryName,
                Description = invalidDescription
            };

            var handler = new CreateCategoryCommandHandler(mockRepository);

            // Assume that Category.Create method returns a failure result
            var expectedResult = Result<Category>.Failure("Name is required");

            mockRepository.AddAsync(Arg.Any<Category>()).Returns(expectedResult);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.ValidationsErrors.Should().NotBeNull().And.NotBeEmpty();
            result.Category.Should().BeNull();
        }

        public void Dispose()
        {
            mockRepository.ClearReceivedCalls();
        }
    }
}
