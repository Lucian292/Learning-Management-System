using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.CreateChapter
{
    public class CreateChapterCommandHandler : IRequestHandler<CreateChapterCommand, CreateChapterCommandResponse>
    {
        private readonly IChapterRepository chapterRepository;
        private readonly ICurrentUserService userService;
        private readonly ICourseRepository courseRepository;

        public CreateChapterCommandHandler(IChapterRepository chapterRepository, ICurrentUserService userService, ICourseRepository courseRepository)
        {
            this.chapterRepository = chapterRepository;
            this.userService = userService;
            this.courseRepository = courseRepository;
        }

        public async Task<CreateChapterCommandResponse> Handle(CreateChapterCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateChapterCommandValidator(userService, courseRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var chapter = Chapter.Create(request.CourseId,request.Title);

/*            if (request.Content != null)
            {
                chapter.Value.AttachContent(request.Content);
            }*/

            if (chapter.IsSuccess)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                chapter.Value.AttachLink(request.Link);
#pragma warning disable CS8604 // Possible null reference argument.
                chapter.Value.AttachContent(request.Content);

                await chapterRepository.AddAsync(chapter.Value);

                return new CreateChapterCommandResponse
                {
                    Success = true,
                    Chapter = new CreateChapterDto
                    {
                        ChapterId = chapter.Value.ChapterId,
                        CourseId = chapter.Value.CourseId,
                        Title = chapter.Value.Title,
                        Link = chapter.Value.Link,
                        Content = chapter.Value.Content
                    }
                };
            }
            return new CreateChapterCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { chapter.Error }
            };

        }

    }
}
