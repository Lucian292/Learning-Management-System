using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.CreatePdf
{
    public class CreatePdfCommandResponse : BaseResponse
    {
        public CreatePdfCommandResponse() : base()
        {
        }

        public CreatePdfDto CreatePdfDto { get; set; }
    }
}
