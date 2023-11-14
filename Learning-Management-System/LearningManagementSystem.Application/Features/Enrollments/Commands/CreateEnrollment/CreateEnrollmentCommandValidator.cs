using FluentValidation;
using LearningManagementSystem.Application.Features.Chapters.Commands.CreateChapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommandValidator : AbstractValidator<CreateEnrollmentCommand>
    {
        public CreateEnrollmentCommandValidator()
        {
            RuleFor(p => p.CourseId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
