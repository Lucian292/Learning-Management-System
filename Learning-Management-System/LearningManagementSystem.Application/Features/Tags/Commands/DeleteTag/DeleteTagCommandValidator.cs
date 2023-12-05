using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
    {
        public DeleteTagCommandValidator()
        {
            RuleFor(p => p.TagId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
