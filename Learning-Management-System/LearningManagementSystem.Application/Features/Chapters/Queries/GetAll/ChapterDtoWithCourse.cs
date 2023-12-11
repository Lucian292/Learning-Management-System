using LearningManagementSystem.Application.Features.Courses.Queries;
using LearningManagementSystem.Application.Features.Questions.Queries.GetQuestionById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Chapters.Queries.GetAll
{
    public class ChapterDtoWithCourse
    {
        public Guid ChapterId { get; set; }
        public CourseDto? Course { get; set; }
        public string Title { get; set; } = default!;
        public string Link { get; set; } = default!;
        public byte[] Content { get; set; } = default!;
        public List<QuestionDto> Questions { get; set; } = new();
    }
}
