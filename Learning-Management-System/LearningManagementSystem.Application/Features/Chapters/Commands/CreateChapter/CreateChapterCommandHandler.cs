using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.CreateChapter
{
    public class CreateChapterCommandHandler : IRequestHandler<CreateChapterCommand, CreateChapterCommandResponse>
    {
        private readonly IChapterRepository repository;

        public CreateChapterCommandHandler(IChapterRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateChapterCommandResponse> Handle(CreateChapterCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateChapterCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var chapter = Chapter.Create(request.CourseId,request.Title/*,request.Link,request.Content*/);
            if (!chapter.IsSuccess)
            {
                return new CreateChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { chapter.Error }
                };
            }

            await repository.AddAsync(chapter.Value);

            return new CreateChapterCommandResponse
            {
                Success = true,
                Chapter = new CreateChapterDto
                {
                    ChapterId = chapter.Value.ChapterId,
                    CourseId = chapter.Value.CourseId,
                    Title = chapter.Value.Title,
                    //Link = chapter.Value.Link,
                    //Content = chapter.Value.Content
                }
            };
        }

    }
}
