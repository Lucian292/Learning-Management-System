using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Application.Features.Chapters.Commands.CreatePdf;
using MediatR;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.UpdateChapter
{
    public class CreatePdfCommandHandler : IRequestHandler<CreatePdfCommand, CreatePdfCommandResponse>
    {
        private readonly IChapterRepository chapterRepository;
        private readonly ICurrentUserService userService;

        public CreatePdfCommandHandler(IChapterRepository chapterRepository, ICurrentUserService userService)
        {
            this.chapterRepository = chapterRepository;
            this.userService = userService;
        }

        public async Task<CreatePdfCommandResponse> Handle(CreatePdfCommand request, CancellationToken cancellationToken)
        {
            var chapter = await chapterRepository.FindByIdAsync(request.ChapterId);

            if (!chapter.IsSuccess)
            {
                return new CreatePdfCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { chapter.Error }
                };
            }

            var userId = Guid.Parse(userService.UserId);

            if (chapter.Value.Course.ProfessorId != userId && !userService.IsUserAdmin())
            {
                return new CreatePdfCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "User doesn't own the course" }
                };
            }

            if (request.File != null)
            {
                // Convertește IFormFile în byte[]
                using var stream = request.File.OpenReadStream();
                var fileBytes = new byte[request.File.Length];
                stream.Read(fileBytes, 0, (int)request.File.Length);

                chapter.Value.AttachContent(fileBytes);
            }

            var result = await chapterRepository.UpdateAsync(chapter.Value);

            if (result.IsSuccess)
            {
                return new CreatePdfCommandResponse
                {
                    Success = true,
                    CreatePdfDto = new CreatePdfDto
                    {
                        Pdf = chapter.Value.Content
                    }
                };
            }

            return new CreatePdfCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { chapter.Error }
            };
        }
    }
}
