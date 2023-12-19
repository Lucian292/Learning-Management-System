using LearningManagementSystem.Application.Features.Choice.Queries;
using LearningManagementSystem.Application.Features.Questions.Queries.GetQuestionById;
using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Chapters.Queries.GetById
{
    public class GetByIdChapterHandler : IRequestHandler<GetByIdChapterQuery, ChapterDto>
    {
        private readonly IChapterRepository repository;

        public GetByIdChapterHandler(IChapterRepository repository)
        {
            this.repository = repository;
        }
        public async Task<ChapterDto> Handle(GetByIdChapterQuery request, CancellationToken cancellationToken)
        {
            var chapter = await repository.FindByIdAsync(request.Id);
            if (chapter.IsSuccess)
            {
                return new ChapterDto
                {
                    ChapterId = chapter.Value.ChapterId,
                    CourseId = chapter.Value.CourseId,
                    Title = chapter.Value.Title,
                    Link = chapter.Value.Link,
                    Content = chapter.Value.Content,
                    Questions = chapter.Value.Quizz.Select(q => new QuestionDto
                    {
                        QuestionId = q.QuestionId,
                        Text = q.Text,
                        Choices = q.Choices.Select(c => new ChoiceDto
                        {
                            ChoiceId = c.ChoiceId,
                            Content = c.Content,
                            IsCorrect = c.IsCorrect
                        }).ToList()
                    }).ToList()
                };
            }
            return new ChapterDto();
        }
    }
}
