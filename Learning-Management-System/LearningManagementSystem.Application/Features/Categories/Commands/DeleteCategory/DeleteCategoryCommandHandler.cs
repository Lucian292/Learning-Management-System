using LearningManagementSystem.Application.Persistence;
using MediatR;

namespace LearningManagementSystem.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryCommandResponse>
    {
        private readonly ICategoryRepository repository;

        public DeleteCategoryCommandHandler(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteCategoryCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new DeleteCategoryCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var category = await repository.FindByIdAsync(request.CategoryId);
            if (!category.IsSuccess)
            {
                return new DeleteCategoryCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { category.Error }
                };
            }

            await repository.DeleteAsync(request.CategoryId);

            return new DeleteCategoryCommandResponse
            {
                Success = true
            };
        }
    }
}
