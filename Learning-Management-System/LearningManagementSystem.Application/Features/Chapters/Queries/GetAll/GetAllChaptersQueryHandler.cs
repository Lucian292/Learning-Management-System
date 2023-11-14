using LearningManagementSystem.Application.Features.Categories.Queries.GetAll;
using LearningManagementSystem.Application.Features.Categories.Queries;
using LearningManagementSystem.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;

namespace LearningManagementSystem.Application.Features.Chapters.Queries.GetAll
{
    public class GetAllChaptersQueryHandler : IRequestHandler<GetAllChaptersQuery, GetAllChaptersResponse>
    {
        private readonly IChapterRepository repository;

        public GetAllChaptersQueryHandler(IChapterRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllChaptersResponse> Handle(GetAllChaptersQuery request, CancellationToken cancellationToken)
        {
            GetAllChaptersResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Chapters = result.Value.Select(chapter => new ChapterDto
                {
                    ChapterId = chapter.ChapterId,
                    CourseId = chapter.CourseId,
                    Title = chapter.Title,
                    Link = chapter.Link,
                    Content = chapter.Content
                }).ToList();
            }
            return response;
        }
    }
}
