using MediatR;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Application.Contracts.Interfaces;

namespace LearningManagementSystem.Application.Features.Chapters.Queries.GetQuizResults
{
    public class GetQuizResultsQueryHandler : IRequestHandler<GetQuizResultsQuery, GetQuizResultsQueryResponse>
    {
        private readonly ICurrentUserService userService;
        private readonly IChapterRepository chapterRepository;
        private readonly IEnrollmentRepository enrollmentRepository;

        public GetQuizResultsQueryHandler(ICurrentUserService userService, IChapterRepository chapterRepository, IEnrollmentRepository enrollmentRepository)
        {
            this.userService = userService;
            this.chapterRepository = chapterRepository;
            this.enrollmentRepository = enrollmentRepository;
        }

        public async Task<GetQuizResultsQueryResponse> Handle(GetQuizResultsQuery request, CancellationToken cancellationToken)
        {
			var validator = new GetQuizResultsQueryValidator(userService, chapterRepository, enrollmentRepository);
            var valResult = await validator.ValidateAsync(request, cancellationToken);

            if (!valResult.IsValid)
                return new GetQuizResultsQueryResponse();

            var userId = Guid.Parse(userService.UserId);

            var enrollment = (await enrollmentRepository.FindByIdAsync(request.EnrollmentId)).Value;

            List<QuizResultDto> results = [];

            foreach (var qResult in enrollment.QuizzResults)
            {
                var right = string.Empty;
                foreach (var c in qResult?.QuestionResult?.Question?.Choices)
                    if (c.IsCorrect)
                    {
                        right = c.Content;
                    }

                if (qResult.QuestionResult.IsCorrect)
                {
                    results.Add(new QuizResultDto { WasCorrect = true, RightChoice = right, Question = qResult.QuestionResult.Question.Text });
                }
                else
                {
                    results.Add(new QuizResultDto { WasCorrect = false, RightChoice = right, Question = qResult.QuestionResult.Question.Text });
                }
            }

            return new GetQuizResultsQueryResponse { Results = results };
        }
    }
}
