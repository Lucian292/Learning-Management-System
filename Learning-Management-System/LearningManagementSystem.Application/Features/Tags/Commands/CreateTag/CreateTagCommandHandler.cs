using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Tags.Commands.CreateTag
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, CreateTagCommandResponse>
    {
        private readonly ITagRepository repository;
        public CreateTagCommandHandler(ITagRepository tagRepository)
        {
            this.repository = tagRepository;
        }

        public async Task<CreateTagCommandResponse> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTagCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateTagCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var tag = Tag.Create(request.Content);
            if (!tag.IsSuccess)
            {
                return new CreateTagCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { tag.Error }
                };
            }

            await repository.AddAsync(tag.Value);

            return new CreateTagCommandResponse
            {
                Success = true,
                Tag = new CreateTagDto
                {
                    TagId = tag.Value.TagId,
                    Content = tag.Value.Content
                }
            };
        }

    }
}
