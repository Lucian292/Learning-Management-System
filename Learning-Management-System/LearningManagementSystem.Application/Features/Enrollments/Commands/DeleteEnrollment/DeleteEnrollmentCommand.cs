using LearningManagementSystem.Application.Features.Chapters.Commands.DeleteChapter;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Enrollments.Commands.DeleteEnrollment
{
    public class DeleteEnrollmentCommand : IRequest<DeleteEnrollmentCommandResponse>
    {
        public Guid EnrollmentId { get; set; }
    }
}
