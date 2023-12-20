using FluentValidation;
using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.CreateChapter
{
    public class CreateChapterCommandValidator : AbstractValidator<CreateChapterCommand>
    {
        private readonly ICurrentUserService userService;
        private readonly ICourseRepository courseRepository;

        public CreateChapterCommandValidator(ICurrentUserService userService, ICourseRepository courseRepository)
        {
            this.userService = userService;
            this.courseRepository = courseRepository;

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.CourseId)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.")
                .MustAsync(async (courseId, cancellationToken) =>
                {
                    if (userService.IsUserAdmin())
                        return true;

                    var userId = Guid.Parse(userService.UserId);

                    var isOwner = await courseRepository.IsCourseOwnedByUserAsync(courseId, userId);
                    return isOwner;
                }).WithMessage("User doesn't own the course");
        }
    }
}
