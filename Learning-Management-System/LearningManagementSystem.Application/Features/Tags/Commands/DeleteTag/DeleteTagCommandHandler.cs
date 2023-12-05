using MediatR;
using LearningManagementSystem.Application.Persistence.Courses;

namespace LearningManagementSystem.Application.Features.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, DeleteTagCommandResponse>
    {
        private readonly ITagRepository tagRepository;

        public DeleteTagCommandHandler(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public async Task<DeleteTagCommandResponse> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteTagCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new DeleteTagCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var tag = await tagRepository.FindByIdAsync(request.TagId);

            if (!tag.IsSuccess)
            {
                return new DeleteTagCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { tag.Error }
                };
            }

            await tagRepository.DeleteAsync(request.TagId);

            return new DeleteTagCommandResponse
            {
                Success = true
            };
        }
    }
}
