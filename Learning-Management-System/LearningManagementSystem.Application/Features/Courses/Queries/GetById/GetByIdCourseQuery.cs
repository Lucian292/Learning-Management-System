using LearningManagementSystem.Application.Features.Categories.Queries;
using MediatR;

namespace LearningManagementSystem.Application.Features.Courses.Queries.GetById
{
    public record GetByIdCourseQuery(Guid Id) : IRequest<CourseDto>;
}
