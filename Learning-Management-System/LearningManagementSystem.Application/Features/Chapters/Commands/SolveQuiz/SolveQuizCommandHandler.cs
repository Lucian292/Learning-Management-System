using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.SolveQuiz
{
    public class SolveQuizCommandHandler : IRequestHandler<SolveQuizCommand, SolveQuizCommandResponse>
    {
        private readonly ICurrentUserService userService;
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IEnrollmentQuestionResultRepository enrollmentQuestionResultRepository;
        private readonly IQuestionResultRepository questionResultRepository;
        private readonly IChapterRepository chapterRepository;

        public SolveQuizCommandHandler(ICurrentUserService userService, IEnrollmentRepository enrollmentRepository, IEnrollmentQuestionResultRepository enrollmentQuestionResultRepository, IQuestionResultRepository questionResultRepository, IChapterRepository chapterRepository)
        {
            this.userService = userService;
            this.enrollmentRepository = enrollmentRepository;
            this.enrollmentQuestionResultRepository = enrollmentQuestionResultRepository;
            this.questionResultRepository = questionResultRepository;
            this.chapterRepository = chapterRepository;
        }

        public async Task<SolveQuizCommandResponse> Handle(SolveQuizCommand request, CancellationToken cancellationToken)
        {
            var validator = new SolveQuizCommandValidator(userService, enrollmentRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new SolveQuizCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var userId = Guid.Parse(userService.UserId);

            var enrollment = (await enrollmentRepository.FindByIdAsync(request.EnrollmentId)).Value;

            var chapter = await chapterRepository.FindByIdAsync(request.ChapterId);

            if (!chapter.IsSuccess)
            {
                return new SolveQuizCommandResponse
                {
                    Success = false,
                    ValidationsErrors = [chapter.Error]
                };
            }

            List<EnrollmentQuestionResultDto> enrollmentQuestionResults = [];

            foreach (var question in request.QuestionResults)
            {
                var questionResult = Domain.Entities.Courses.QuestionResult.Create(question.QuestionId, userId, question.IsCorrect);

                if (!questionResult.IsSuccess)
                {
                    return new SolveQuizCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = [questionResult.Error]
                    };
                }

                await questionResultRepository.AddAsync(questionResult.Value);

                var res = await enrollmentQuestionResultRepository.AddAsync(
                    new EnrollmentQuestionResult
                            {
                                ChapterId = chapter.Value.ChapterId,
                                EnrollmentId = enrollment.EnrollmentId,
                                QuestionResultId = questionResult.Value.QuestionResultId,
                                Chapter = chapter.Value,
                                Enrollment = enrollment,
                                QuestionResult = questionResult.Value,
                            });

                if (!res.IsSuccess)
                {
                    return new SolveQuizCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = [res.Error]
                    };
                }

                enrollmentQuestionResults.Add(new EnrollmentQuestionResultDto { QuestionResultId = questionResult.Value.QuestionResultId });
            }

            return new SolveQuizCommandResponse {
                Success = true,
                QuestionResults = enrollmentQuestionResults
            };
        }
    }
}
