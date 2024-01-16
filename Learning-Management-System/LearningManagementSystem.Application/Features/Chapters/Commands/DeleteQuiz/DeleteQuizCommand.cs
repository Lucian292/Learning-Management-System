using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.DeleteQuiz
{
    public class DeleteQuizCommand : IRequest<DeleteQuizCommandResponse>
    {
        public Guid ChapterId { get; set; }
    }
}
