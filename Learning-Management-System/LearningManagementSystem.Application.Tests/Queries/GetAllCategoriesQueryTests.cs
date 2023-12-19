using FluentAssertions;
using LearningManagementSystem.Application.Features.Categories.Queries.GetAll;
using LearningManagementSystem.Application.Persistence;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Queries
{
    public class GetAllCategoriesQueryTests : IDisposable
    {
        private readonly ICategoryRepository _mockCategoryRepository;

        public GetAllCategoriesQueryTests()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
        }

        [Fact]
        public void GetAllCategoriesHandler_Success()
        {
            // Arrange
            var handler = new GetAllCategoriesQueryHandler(_mockCategoryRepository);

            // Act
            var result = handler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().NotBeNull();
            result.Result.Categories.Should().NotBeNull().And.HaveCount(2);
        }

        public void Dispose()
        {
            _mockCategoryRepository.ClearReceivedCalls();
        }
    }
}
