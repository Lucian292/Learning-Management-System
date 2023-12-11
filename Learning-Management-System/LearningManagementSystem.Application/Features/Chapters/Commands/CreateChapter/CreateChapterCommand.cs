using MediatR;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.CreateChapter
{
    public class CreateChapterCommand : IRequest<CreateChapterCommandResponse>
    {
        public string Title { get; set; } = default!;
        public Guid CourseId { get; set; } = default!;
        public string? Link { get; set; }
        public byte[]? Content { get; set; }
    }
}
