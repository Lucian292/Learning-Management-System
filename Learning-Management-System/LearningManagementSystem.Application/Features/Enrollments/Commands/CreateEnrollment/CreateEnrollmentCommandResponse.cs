using LearningManagementSystem.Application.Features.Chapters.Commands.CreateChapter;
using LearningManagementSystem.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommandResponse : BaseResponse
    {
        public CreateEnrollmentCommandResponse() : base()
        {
        }

        public CreateEnrollmentDto Enrollment { get; set; }
    }
}
