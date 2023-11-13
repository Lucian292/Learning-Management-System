

using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Choice.Queries.GetById
{
    public class GetByIdChoiceQueryHandler : IRequestHandler<GetByIdChoiceQuery, ChoiceDto>
    {
        private readonly IChoiceRepository repository;

        public GetByIdChoiceQueryHandler(IChoiceRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ChoiceDto> Handle(GetByIdChoiceQuery request, CancellationToken cancellationToken)
        {
            var choiceResult = await repository.FindByIdAsync(request.Id);
            if (choiceResult.IsSuccess)
            {
                var choice = choiceResult.Value;

                return new ChoiceDto
                {
                    ChoiceId = choice.ChoiceId,
                    Content = choice.Content,
                    IsCorrect = choice.IsCorrect,
                    QuestionId = choice.QuestionId
                };
            }
            return new ChoiceDto();
        }
    }
}
