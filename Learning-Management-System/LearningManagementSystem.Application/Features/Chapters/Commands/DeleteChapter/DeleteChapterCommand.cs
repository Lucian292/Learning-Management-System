using LearningManagementSystem.Application.Features.Categories.Commands.DeleteCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.DeleteChapter
{
    public class DeleteChapterCommand : IRequest<DeleteChapterCommandResponse>
    {
        public Guid ChapterId { get; set; }
    }
}
