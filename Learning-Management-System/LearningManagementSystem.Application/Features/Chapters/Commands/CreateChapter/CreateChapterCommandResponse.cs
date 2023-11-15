using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.CreateChapter
{
    public class CreateChapterCommandResponse : BaseResponse
    {
        public CreateChapterCommandResponse() : base()
        {
        }

        public CreateChapterDto Chapter { get; set; }
    }
}
