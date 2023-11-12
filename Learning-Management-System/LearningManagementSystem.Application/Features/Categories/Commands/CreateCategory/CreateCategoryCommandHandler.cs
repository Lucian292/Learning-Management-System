using LearningManagementSystem.Application.Persistence;
using LearningManagementSystem.Domain.Entities;
using MediatR;

namespace LearningManagementSystem.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
    {
        private readonly ICategoryRepository repository;

        public CreateCategoryCommandHandler(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCategoryCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateCategoryCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var category = Category.Create(request.CategoryName);
            if (!category.IsSuccess)
            {
                return new CreateCategoryCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { category.Error }
                };
            }

            await repository.AddAsync(category.Value);

            return new CreateCategoryCommandResponse
            {
                Success = true,
                Category = new CreateCategoryDto
                {
                    CategoryId = category.Value.CategoryId,
                    CategoryName = category.Value.CategoryName
                }
            };
        }
    }
}
