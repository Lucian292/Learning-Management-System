using FluentValidation;
using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Chapters.Commands.SolveQuiz
{
    public class SolveQuizCommandValidator : AbstractValidator<SolveQuizCommand>
    {
        private readonly ICurrentUserService userService;
        private readonly IEnrollmentRepository enrollmentRepository;

        public SolveQuizCommandValidator(ICurrentUserService userService, IEnrollmentRepository enrollmentRepository)
        {
            this.userService = userService;
            this.enrollmentRepository = enrollmentRepository;

            RuleFor(p => p.EnrollmentId)
                .MustAsync(async (enrollmentId, cancellationToken) =>
                {
                    var userId = Guid.Parse(userService.UserId);
                    var enrollment = await enrollmentRepository.FindByIdAsync(enrollmentId);

                    if (userId != enrollment.Value.UserId)
                        return false;

                    if (enrollment.Value.QuizzResults.Count != 0)
                        return false;

                    return true;
                }).WithMessage("Already solved or Not enrolled");

            RuleFor(p => p.QuestionResults.Count)
                .Equal(10)
                .WithMessage("Not 10 questions!");
        }
    }
}
