using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.CreatePdf
{
    public class CreatePdfCommandValidator : AbstractValidator<CreatePdfCommand>
    {
        public CreatePdfCommandValidator()
        {
            RuleFor(p => p.File)
                .Must(file => BeAValidPdfFile(file))
                .WithMessage("Invalid PDF file. File must be a PDF and should not exceed 15 MB.");
        }

        private bool BeAValidPdfFile(IFormFile pdfFile)
        {
            // Verifică dacă fișierul este PDF și are o dimensiune mai mică sau egală cu 15 MB
            if (pdfFile == null || pdfFile.Length == 0)
            {
                // Fisierul nu este obligatoriu, deci este valid dacă nu există sau are dimensiunea zero
                return true;
            }

            // Convertește IFormFile în byte[]
            using var stream = pdfFile.OpenReadStream();
            var fileBytes = new byte[pdfFile.Length];
            stream.Read(fileBytes, 0, (int)pdfFile.Length);

            // Verifică dacă este un fișier PDF
            return IsPdfFile(fileBytes) && pdfFile.Length <= 15 * 1024 * 1024; // 15 MB
        }

        private bool IsPdfFile(byte[] file)
        {
            const string pdfSignature = "%PDF";
            return file.Length >= pdfSignature.Length &&
                   Encoding.ASCII.GetString(file, 0, pdfSignature.Length) == pdfSignature;
        }
    }
}
