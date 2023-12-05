using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Enrollments.Queries.GetById
{
    public class GetByIdEnrollmentHandler : IRequestHandler<GetByIdEnrollmentQuery, EnrollmentDto>
    {
        private readonly IEnrollmentRepository repository;

        public GetByIdEnrollmentHandler(IEnrollmentRepository repository)
        {
            this.repository = repository;
        }
        public async Task<EnrollmentDto> Handle(GetByIdEnrollmentQuery request, CancellationToken cancellationToken)
        {
            var enrollment = await repository.FindByIdAsync(request.Id);
            if (enrollment.IsSuccess)
            {
                return new EnrollmentDto
                {
                    UserId = enrollment.Value.UserId,
                    CourseId = enrollment.Value.CourseId,
                    Progress = enrollment.Value.Progress
                };
            }
            return new EnrollmentDto();
        }
    }
}