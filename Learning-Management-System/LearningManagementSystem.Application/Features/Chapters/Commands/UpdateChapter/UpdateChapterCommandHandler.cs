using MediatR;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Features.Chapters.Commands.DeleteChapter;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.UpdateChapter
{
    public class UpdateChapterCommandHandler : IRequestHandler<UpdateChapterCommand, UpdateChapterCommandResponse>
    {
        private readonly IChapterRepository chapterRepository;
        private readonly ICurrentUserService userService;

        public UpdateChapterCommandHandler(IChapterRepository chapterRepository, ICurrentUserService userService)
        {
            this.chapterRepository = chapterRepository;
            this.userService = userService;
        }

        public async Task<UpdateChapterCommandResponse> Handle(UpdateChapterCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateChapterCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new UpdateChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var chapter = await chapterRepository.FindByIdAsync(request.ChapterId);

            if (!chapter.IsSuccess)
            {
                return new UpdateChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { chapter.Error }
                };
            }

            var userId = Guid.Parse(userService.UserId);

            if (chapter.Value.Course.ProfessorId != userId && !userService.IsUserAdmin())
            {
                return new UpdateChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "User doesn't own the course" }
                };
            }

            chapter.Value.Update(request.Chapter.Title, request.Chapter.Link); /*,request.Link,request.Content*/

            if (request.Chapter.Content != null)
            {
                chapter.Value.AttachContent(request.Chapter.Content);
            }

            var result = await chapterRepository.UpdateAsync(chapter.Value);

            if (result.IsSuccess)
            {

                return new UpdateChapterCommandResponse
                {
                    Success = true,
                    Chapter = new UpdateChapterDto
                    {
                        Title = chapter.Value.Title,
                        Link = chapter.Value.Link,
                        Content = chapter.Value.Content
                    }
                };
            }

            return new UpdateChapterCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { chapter.Error }
            };
        }
    }
}
