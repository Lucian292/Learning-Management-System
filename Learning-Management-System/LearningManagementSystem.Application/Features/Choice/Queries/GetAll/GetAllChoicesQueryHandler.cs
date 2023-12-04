using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Choice.Queries.GetAll
{
    public class GetAllChoicesQueryHandler : IRequestHandler<GetAllChoicesQuery, GetAllChoicesResponse>
    {
        private readonly IChoiceRepository _choiceRepository;

        public GetAllChoicesQueryHandler(IChoiceRepository choiceRepository)
        {
            _choiceRepository = choiceRepository;
        }

        public async Task<GetAllChoicesResponse> Handle(GetAllChoicesQuery request, CancellationToken cancellationToken)
        {
            GetAllChoicesResponse response = new();
            var result = await _choiceRepository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Choices = result.Value.Select(c => new ChoiceDto
                {
                    ChoiceId = c.ChoiceId,
                    Content = c.Content,
                    IsCorrect = c.IsCorrect,
                    QuestionId = c.QuestionId
                }).ToList();
            }
            return response;
        }
    }
}
