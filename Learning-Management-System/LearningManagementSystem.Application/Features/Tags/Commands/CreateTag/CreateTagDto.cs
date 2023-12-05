namespace LearningManagementSystem.Application.Features.Tags.Commands.CreateTag
{
    public class CreateTagDto
    {
        public Guid TagId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
