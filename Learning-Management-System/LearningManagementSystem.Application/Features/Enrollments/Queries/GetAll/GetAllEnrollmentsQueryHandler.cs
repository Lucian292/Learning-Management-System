using MediatR;
using LearningManagementSystem.Application.Persistence.Courses;

namespace LearningManagementSystem.Application.Features.Enrollments.Queries.GetAll
{
    public class GetAllEnrollmentsQueryHandler : IRequestHandler<GetAllEnrollmentsQuery, GetAllEnrollmentsResponse>
    {
        private readonly IEnrollmentRepository repository;

        public GetAllEnrollmentsQueryHandler(IEnrollmentRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllEnrollmentsResponse> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            GetAllEnrollmentsResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Enrollments = result.Value.Select(enrollment => new EnrollmentDto
                {
                    UserId = enrollment.UserId,
                    CourseId = enrollment.CourseId,
                    Progress = enrollment.Progress
                }).ToList();
            }
            return response;
        }
    }
}