using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Application.Features.Courses.Queries;

namespace LearningManagementSystem.Application.Features.Tags.Queries.GetById
{
    public class GetByIdTagQueryHandler : IRequestHandler<GetByIdTagQuery, TagDto>
    {
        private readonly ITagRepository tagRepository;

        public GetByIdTagQueryHandler(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public async Task<TagDto> Handle(GetByIdTagQuery request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.FindByIdAsync(request.TagId);
            if (tag.IsSuccess)
            {
                return new TagDto
                {
                    TagId = tag.Value.TagId,
                    Content = tag.Value.Content,
                };
            }
            return new TagDto();
        }
    }
}
