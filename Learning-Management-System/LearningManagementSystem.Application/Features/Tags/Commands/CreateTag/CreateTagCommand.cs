using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Tags.Commands.CreateTag
{
    public class CreateTagCommand : IRequest<CreateTagCommandResponse>
    {
        public string Content { get; set; } = string.Empty;
    }
}
