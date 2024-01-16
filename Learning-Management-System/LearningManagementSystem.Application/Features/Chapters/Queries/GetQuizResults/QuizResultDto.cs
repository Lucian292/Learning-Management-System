namespace LearningManagementSystem.Application.Features.Chapters.Queries.GetQuizResults
{
    public class QuizResultDto
    {
        public bool WasCorrect {  get; set; }
        public string RightChoice { get; set; } = string.Empty;
        public string Question { get; set; } = string.Empty;
    }
}