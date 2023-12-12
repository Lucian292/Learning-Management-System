using LearningManagementSystem.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.UpdateChapter
{
    public class UpdateChapterCommandResponse : BaseResponse
    {
        public UpdateChapterDto Chapter { get; set; } = new();
        public UpdateChapterCommandResponse(): base() { }
    }
}
