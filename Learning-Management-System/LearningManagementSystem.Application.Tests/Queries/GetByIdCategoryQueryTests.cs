using FluentAssertions;
using LearningManagementSystem.Application.Features.Categories;
using LearningManagementSystem.Application.Features.Categories.Queries;
using LearningManagementSystem.Application.Features.Categories.Queries.GetById;
using LearningManagementSystem.Application.Persistence;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Queries
{
    public class GetByIdCategoryQueryTests : IDisposable
    {
        private readonly ICategoryRepository mockCategoryRepository;

        public GetByIdCategoryQueryTests()
        {
            this.mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
        }
        [Fact]
        public async Task GetCategoryByIdHandler_WithValidId_ReturnsCategory()
        {
            // Arrange
            var expectedCategory = Category.Create("Test Category").Value;
            expectedCategory.AttachDescription("Test Description");
            var categoryId = expectedCategory.CategoryId;

            mockCategoryRepository.FindByIdAsync(categoryId).Returns(Task.FromResult(Result<Category>.Success(expectedCategory)));

            var handler = new GetByIdCategoryHandler(mockCategoryRepository);

            // Act
            var result = await handler.Handle(new GetByIdCategoryQuery(categoryId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new CategoryDto { CategoryId = expectedCategory.CategoryId,
                                                             CategoryName = expectedCategory.CategoryName,
                                                             Description = expectedCategory.Description
                                                            });
        }

        [Fact]
        public async Task GetCategoryByIdHandler_WithInvalidId_ReturnsFailure()
        {
            // Arrange
            var invalidCategoryId = new Guid("a1b2c3d4-5e6f-7a8b-9c0d-1e2f3a4b5678");

            mockCategoryRepository.FindByIdAsync(invalidCategoryId).Returns(Task.FromResult(Result<Category>.Failure("Category not found")));

            var handler = new GetByIdCategoryHandler(mockCategoryRepository);

            // Act
            var result = await handler.Handle(new GetByIdCategoryQuery(invalidCategoryId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new CategoryDto());
        }

        public void Dispose()
        {
            mockCategoryRepository.ClearReceivedCalls();
        }
    }
}
