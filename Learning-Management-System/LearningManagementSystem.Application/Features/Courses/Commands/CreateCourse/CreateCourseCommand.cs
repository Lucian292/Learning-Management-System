

using LearningManagementSystem.Application.Features.Categories.Commands.CreateCategory;
using MediatR;

namespace LearningManagementSystem.Application.Features.Couerses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest<CreateCourseCommandResponse>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
    }
}
