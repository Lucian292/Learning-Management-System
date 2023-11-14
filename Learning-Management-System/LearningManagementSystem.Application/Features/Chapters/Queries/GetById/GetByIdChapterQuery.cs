using MediatR;

namespace LearningManagementSystem.Application.Features.Chapters.Queries.GetById
{
    public record GetByIdChapterQuery(Guid Id) : IRequest<ChapterDto>;
}
