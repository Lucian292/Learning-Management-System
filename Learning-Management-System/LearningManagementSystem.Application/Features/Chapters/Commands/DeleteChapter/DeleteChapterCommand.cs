using MediatR;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.DeleteChapter
{
    public class DeleteChapterCommand : IRequest<DeleteChapterCommandResponse>
    {
        public Guid ChapterId { get; set; }
    }
}
