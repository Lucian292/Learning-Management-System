using LearningManagementSystem.Application.Features.Choice.Queries;
using LearningManagementSystem.Application.Features.Questions.Queries.GetQuestionById;
using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Questions.Queries.GetAll
{
    public class GetAllQuestionQueryHandler : IRequestHandler<GetAllQuestionQuery, GetAllQuestionResponse>
    {
        private readonly IQuestionRepository questionRepository;

        public GetAllQuestionQueryHandler(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public async Task<GetAllQuestionResponse> Handle(GetAllQuestionQuery request, CancellationToken cancellationToken)
        {
            GetAllQuestionResponse response = new();
            var result = await questionRepository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Questions = result.Value.Select(c => new QuestionDto
                {
                    QuestionId = c.QuestionId,
                    Text = c.Text,
                    Choices = c.Choices.Select(c => new ChoiceDto
                    {
                        ChoiceId = c.ChoiceId,
                        Content = c.Content,
                        IsCorrect = c.IsCorrect
                    }).ToList()
                }).ToList();
            }
            return response;
        }
    }
}
