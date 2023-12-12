using MediatR;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.UpdateChapter
{
    public class UpdateChapterCommand : IRequest<UpdateChapterCommandResponse>
    {
        public Guid ChapterId { get; set; }
        public UpdateChapterDto? Chapter { get; set; }
    }
}
