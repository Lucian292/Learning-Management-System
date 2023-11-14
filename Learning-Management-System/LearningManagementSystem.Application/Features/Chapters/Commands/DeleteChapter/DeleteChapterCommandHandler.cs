using LearningManagementSystem.Application.Features.Categories.Commands.DeleteCategory;
using LearningManagementSystem.Application.Persistence;
using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.DeleteChapter
{
    public class DeleteChapterCommandHandler : IRequestHandler<DeleteChapterCommand, DeleteChapterCommandResponse>
    {
        private readonly IChapterRepository repository;

        public DeleteChapterCommandHandler(IChapterRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteChapterCommandResponse> Handle(DeleteChapterCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteChapterCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new DeleteChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var chapter = await repository.FindByIdAsync(request.ChapterId);
            if (!chapter.IsSuccess)
            {
                return new DeleteChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { chapter.Error }
                };
            }

            await repository.DeleteAsync(request.ChapterId);

            return new DeleteChapterCommandResponse
            {
                Success = true
            };
        }
    }
}
