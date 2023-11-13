
using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Questions.Commands.DeleteQuestion
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, DeleteQuestionCommandResponse>
    {
        private readonly IQuestionRepository questionRepository;

        public DeleteQuestionCommandHandler(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public async Task<DeleteQuestionCommandResponse> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteQuestionCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new DeleteQuestionCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var question = await questionRepository.FindByIdAsync(request.QuestionId);
            if (!question.IsSuccess)
            {
                return new DeleteQuestionCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { question.Error }
                };
            }

            await questionRepository.DeleteAsync(request.QuestionId);

            return new DeleteQuestionCommandResponse
            {
                Success = true
            };
        }
    }
}
