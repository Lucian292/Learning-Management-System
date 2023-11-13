using LearningManagementSystem.Application.Features.Choice.Queries;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Questions.Queries.GetQuestionById
{
    public class GetByIdQuestionQueryHandler : IRequestHandler<GetByIdQuestionQuery, QuestionDto>
    {
        private readonly IQuestionRepository questionRepository;

        public GetByIdQuestionQueryHandler(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public async Task<QuestionDto> Handle(GetByIdQuestionQuery request, CancellationToken cancellationToken)
        {
            var questionResult = await questionRepository.FindByIdAsync(request.Id);

            if (questionResult.IsSuccess)
            {
                var question = questionResult.Value;

                return new QuestionDto
                {
                    QuestionId = question.QuestionId,
                    Text = question.Text,
                    Choices = question.Choices.Select(c => new ChoiceDto
                    {
                        ChoiceId = c.ChoiceId,
                        Content = c.Content,
                        IsCorrect = c.IsCorrect
                    }).ToList()
                };
            }
            return new QuestionDto();
        }
    }
}
