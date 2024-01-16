using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.DeleteQuiz
{
    public class DeleteQuizCommandValidator : AbstractValidator<DeleteQuizCommand>
    {
        public DeleteQuizCommandValidator()
        {
            RuleFor(p => p.ChapterId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
