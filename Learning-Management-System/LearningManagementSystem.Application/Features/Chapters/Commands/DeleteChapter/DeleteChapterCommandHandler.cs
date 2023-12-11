using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.DeleteChapter
{
    public class DeleteChapterCommandHandler : IRequestHandler<DeleteChapterCommand, DeleteChapterCommandResponse>
    {
        private readonly IChapterRepository repository;
        private readonly ICurrentUserService userService;

        public DeleteChapterCommandHandler(IChapterRepository repository, ICurrentUserService userService)
        {
            this.repository = repository;
            this.userService = userService;
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

            var userId = Guid.Parse(userService.UserId);

            if (chapter.Value.Course.ProfessorId != userId && !userService.IsUserAdmin())
            {
                return new DeleteChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "User doesn't own the course" }
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
