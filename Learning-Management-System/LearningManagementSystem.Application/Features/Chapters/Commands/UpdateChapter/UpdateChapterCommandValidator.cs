using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.UpdateChapter
{
    public class UpdateChapterCommandValidator : AbstractValidator<UpdateChapterCommand>
    {
        public UpdateChapterCommandValidator()
        {
            RuleFor(p => p.Chapter.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.Chapter.Content)
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
            return IsPdfFile(pdfFile) && pdfFile.Length <= 15 * 1024 * 1024; // 15 MB
        }

        private bool IsPdfFile(byte[] file)
        {
            const string pdfSignature = "%PDF";
            return file.Length >= pdfSignature.Length &&
                   Encoding.ASCII.GetString(file, 0, pdfSignature.Length) == pdfSignature;
        }
    }
}
