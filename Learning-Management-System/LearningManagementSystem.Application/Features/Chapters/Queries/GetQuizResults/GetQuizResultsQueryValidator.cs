using FluentValidation;
using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;

namespace LearningManagementSystem.Application.Features.Chapters.Queries.GetQuizResults
{
    public class GetQuizResultsQueryValidator : AbstractValidator<GetQuizResultsQuery>
    {
        private readonly ICurrentUserService userService;
        private readonly IChapterRepository chapterRepository;
        private readonly IEnrollmentRepository enrollmentRepository;

        public GetQuizResultsQueryValidator(ICurrentUserService userService, IChapterRepository chapterRepository, IEnrollmentRepository enrollmentRepository)
        {
            this.userService = userService;
            this.chapterRepository = chapterRepository;
            this.enrollmentRepository = enrollmentRepository;

            RuleFor(p => p)
                .MustAsync(async (p, cancellationToken) =>
                {
                    var userId = Guid.Parse(userService.UserId);

                    var res1 = await enrollmentRepository.FindByIdAsync(p.EnrollmentId);

                    if (!res1.IsSuccess)
                        return false;

                    var enrollment = res1.Value;

                    if (enrollment.UserId != userId)
                        return false;

                    var res2 = await chapterRepository.FindByIdAsync(p.ChapterId);

                    if (!res2.IsSuccess)
                        return false;

                    var chapter = res2.Value;

                    if (chapter.Course?.CourseId != enrollment.CourseId)
                        return false;

                    return true;
                }).WithMessage("Invalid query");
        }
    }
}
