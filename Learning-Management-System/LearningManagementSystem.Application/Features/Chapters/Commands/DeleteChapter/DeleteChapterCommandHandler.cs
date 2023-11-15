using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

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
