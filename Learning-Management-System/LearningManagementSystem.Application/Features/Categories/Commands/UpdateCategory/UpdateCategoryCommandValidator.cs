using FluentValidation;

namespace LearningManagementSystem.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.UpdateCategoryDto.CategoryName)
                .NotEmpty().WithMessage("Category name is required")
                .MaximumLength(255).WithMessage("Category name must not exceed 255 characters");
            
        }
    }
}
