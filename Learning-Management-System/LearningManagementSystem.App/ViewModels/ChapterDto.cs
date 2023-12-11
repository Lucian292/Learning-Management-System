namespace LearningManagementSystem.App.ViewModels
{
    public class ChapterDto
    {
        public Guid ChapterId { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<QuestionDto> quizz { get; set; } = new List<QuestionDto>();


    }
}
