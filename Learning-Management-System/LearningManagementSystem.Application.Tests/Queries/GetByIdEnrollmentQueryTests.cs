using FluentAssertions;
using LearningManagementSystem.Application.Features.Enrollments.Queries;
using LearningManagementSystem.Application.Features.Enrollments.Queries.GetById;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Common;
using LearningManagementSystem.Domain.Entities.Courses;
using NSubstitute;

namespace LearningManagementSystem.Application.Tests.Queries
{
    public class GetByIdEnrollmentQueryTests : IDisposable
    {
        private readonly IEnrollmentRepository mockEnrollmentRepository;

        public GetByIdEnrollmentQueryTests()
        {
            this.mockEnrollmentRepository = RepositoryMocks.GetEnrollmentRepository();
        }

        [Fact]
        public async Task GetByIdEnrollmentHandler_WithValidId_ReturnsEnrollment()
        {
            // Arrange
            var userId = new Guid("c7e4a9d2-3b1a-4a2e-9f34-8f2a9f901123");
            var courseId = new Guid("84d9672b-9f45-4820-a37c-6a2898c13245");
            var expectedEnrollment = Enrollment.Create(userId, courseId).Value;

            mockEnrollmentRepository.FindByIdAsync(expectedEnrollment.EnrollmentId).Returns(Task.FromResult(Result<Enrollment>.Success(expectedEnrollment)));

            var handler = new GetByIdEnrollmentHandler(mockEnrollmentRepository);

            // Act
            var result = await handler.Handle(new GetByIdEnrollmentQuery(expectedEnrollment.EnrollmentId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new EnrollmentDto
            {
                UserId = expectedEnrollment.UserId,
                CourseId = expectedEnrollment.CourseId,
                Progress = expectedEnrollment.Progress
            });
        }

        [Fact]
        public async Task GetByIdEnrollmentHandler_WithInvalidId_ReturnsFailure()
        {
            // Arrange
            var invalidEnrollmentId = new Guid("a1b2c3d4-5e6f-7a8b-9c0d-1e2f3a4b5678");

            var mockEnrollmentRepository = Substitute.For<IEnrollmentRepository>();
            mockEnrollmentRepository.FindByIdAsync(invalidEnrollmentId).Returns(Task.FromResult(Result<Enrollment>.Failure("Enrollment not found")));

            var handler = new GetByIdEnrollmentHandler(mockEnrollmentRepository);

            // Act
            var result = await handler.Handle(new GetByIdEnrollmentQuery(invalidEnrollmentId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new EnrollmentDto());
        }

        public void Dispose()
        {
            mockEnrollmentRepository.ClearReceivedCalls();
        }
    }
}
