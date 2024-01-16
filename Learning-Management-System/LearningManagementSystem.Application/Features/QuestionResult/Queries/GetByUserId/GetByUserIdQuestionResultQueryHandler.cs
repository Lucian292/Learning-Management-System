using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.QuestionResult.Queries.GetByUserId
{
    public class GetByUserIdQuestionResultQueryHandler : IRequestHandler<GetByUserIdQuestionResultQuery, GetByUserIdQuestionResultResponse>
    {
        private readonly IQuestionResultRepository questionResultRepository;
        private readonly ICurrentUserService currentUserService;

        public GetByUserIdQuestionResultQueryHandler(IQuestionResultRepository questionResultRepository, ICurrentUserService currentUserService)
        {
            this.questionResultRepository = questionResultRepository ?? throw new ArgumentNullException(nameof(questionResultRepository));
            this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<GetByUserIdQuestionResultResponse> Handle(GetByUserIdQuestionResultQuery request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(currentUserService.UserId);

            var response = new GetByUserIdQuestionResultResponse();

            var result = await questionResultRepository.GetByUserIdAsync(userId);

            if (result.IsSuccess)
            {
                response.QuestionResults = result.Value.Select(qr => new QuestionResultDto
                {
                    QuestionResultId = qr.QuestionResultId,
                    QuestionId = qr.QuestionId,
                    IsCorrect = qr.IsCorrect,
                    UserId = qr.UserId
                }).ToList();
            }

            return response;
        }
    }
}
