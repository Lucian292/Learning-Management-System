using MediatR;

namespace LearningManagementSystem.Application.Features.Questions.Queries.GetQuestionById
{
    public record GetByIdQuestionQuery(Guid Id) : IRequest<QuestionDto>;
  
}
