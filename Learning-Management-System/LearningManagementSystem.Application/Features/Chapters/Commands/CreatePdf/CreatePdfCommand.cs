using LearningManagementSystem.Application.Features.Chapters.Commands.CreatePdf;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LearningManagementSystem.Application.Features.Chapters.Commands
{
    public class CreatePdfCommand : IRequest<CreatePdfCommandResponse>
    {
        public Guid ChapterId { get; set; }
        public IFormFile File { get; set; }
    }
}
