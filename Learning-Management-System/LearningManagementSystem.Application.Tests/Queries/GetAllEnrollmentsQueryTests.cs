using FluentAssertions;
using LearningManagementSystem.Application.Features.Enrollments.Queries.GetAll;
using LearningManagementSystem.Application.Persistence.Courses;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Queries
{
    public class GetAllEnrollmentsQueryTests : IDisposable
    {
        private readonly IEnrollmentRepository _mockEnrollmentRepository;

        public GetAllEnrollmentsQueryTests()
        {
            _mockEnrollmentRepository = RepositoryMocks.GetEnrollmentRepository();
        }

        [Fact]
        public async Task GetAllEnrollmentsHandler_Success()
        {
            // Arrange
            var handler = new GetAllEnrollmentsQueryHandler(_mockEnrollmentRepository);

            // Act
            var result = await handler.Handle(new GetAllEnrollmentsQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Enrollments.Should().NotBeNull();
            result.Enrollments.Should().HaveCount(2);
        }

        public void Dispose()
        {
            _mockEnrollmentRepository.ClearReceivedCalls();
        }
    }
}
