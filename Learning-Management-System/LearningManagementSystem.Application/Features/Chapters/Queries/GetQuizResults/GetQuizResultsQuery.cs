using MediatR;

namespace LearningManagementSystem.Application.Features.Chapters.Queries.GetQuizResults
{
    public class GetQuizResultsQuery : IRequest<GetQuizResultsQueryResponse>
    {
        public Guid EnrollmentId { get; set; }
        public Guid ChapterId { get; set; }
    }
}
