using FluentValidation;
using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;
using System.Text;

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

            RuleFor(p => p.Content)
               .Must(content => BeAValidPdfFile(content))
               .WithMessage("Invalid PDF file. File must be a PDF and should not exceed 15 MB.");
        }

        private bool BeAValidPdfFile(byte[] pdfFile)
        {
            // Verifică dacă fișierul este PDF și are o dimensiune mai mică sau egală cu 15 MB
            if (pdfFile == null || pdfFile.Length == 0)
            {
                // Fisierul nu este obligatoriu, deci este valid dacă nu există sau are dimensiunea zero
                return true;
            }

            // Verifică dacă este un fișier PDF cu dimensiunea mai mică sau egală cu 15 MB
            return IsPdfFile(pdfFile) && pdfFile.Length <= 3 * 1024 * 1024; // 15 MB
        }

        private bool IsPdfFile(byte[] file)
        {
            const string pdfSignature = "%PDF";
            return file.Length >= pdfSignature.Length &&
                   Encoding.ASCII.GetString(file, 0, pdfSignature.Length) == pdfSignature;
        }
    }
}
