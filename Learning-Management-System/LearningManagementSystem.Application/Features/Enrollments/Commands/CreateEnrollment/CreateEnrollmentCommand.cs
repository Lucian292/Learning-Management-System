using LearningManagementSystem.Application.Features.Chapters.Commands.CreateChapter;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommand : IRequest<CreateEnrollmentCommandResponse>
    {
        public Guid CourseId { get; set; } = default!;
        public Guid UserId { get; set; } = default!;
        public decimal Progress { get; set; } = default!;
    }
}
