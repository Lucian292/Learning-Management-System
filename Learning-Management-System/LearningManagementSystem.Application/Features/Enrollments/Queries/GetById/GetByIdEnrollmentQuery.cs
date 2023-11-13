using MediatR;

namespace LearningManagementSystem.Application.Features.Enrollments.Queries.GetById
{
    public record GetByIdEnrollmentQuery(Guid Id) : IRequest<EnrollmentDto>;
}