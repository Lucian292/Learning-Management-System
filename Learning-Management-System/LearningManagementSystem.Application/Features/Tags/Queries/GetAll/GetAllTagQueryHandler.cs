using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Tags.Queries.GetAll
{
    public class GetAllTagQueryHandler : IRequestHandler<GetAllTagQuery, GetAllTagResponse>
    {
        private readonly ITagRepository _tagRepository;
        public GetAllTagQueryHandler(ITagRepository tagRepository)
        {
            this._tagRepository = tagRepository;
        }

        public async Task<GetAllTagResponse> Handle(GetAllTagQuery request, CancellationToken cancellationToken)
        {
			GetAllTagResponse response = new();
            var result = await _tagRepository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Tags = result.Value.Select(x => new TagDto
                {
                    TagId = x.TagId,
                    Content= x.Content
                }).ToList();
            }

            return response;
        }
    }
}
