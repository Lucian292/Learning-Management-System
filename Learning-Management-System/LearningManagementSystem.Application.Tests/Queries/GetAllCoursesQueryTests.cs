using FluentAssertions;
using LearningManagementSystem.Application.Features.Courses.Queries.GetAll;
using LearningManagementSystem.Application.Persistence.Courses;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Queries
{
    public class GetAllCoursesQueryTests : IDisposable
    {
        private readonly ICourseRepository _mockCourseRepository;

        public GetAllCoursesQueryTests()
        {
            _mockCourseRepository = RepositoryMocks.GetCourseRepository();
        }

        [Fact]
        public async Task GetAllCoursesHandler_Success()
        {
            // Arrange
            var handler = new GetAllCoursesQueryHandler(_mockCourseRepository);

            // Act
            var result = await handler.Handle(new GetAllCoursesQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Courses.Should().NotBeNull().And.HaveCount(2);
        }

        public void Dispose()
        {
            _mockCourseRepository.ClearReceivedCalls();
        }
    }
}
