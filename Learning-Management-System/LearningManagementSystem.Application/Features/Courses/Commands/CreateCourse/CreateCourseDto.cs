﻿namespace LearningManagementSystem.Application.Features.Couerses.Commands.CreateCourse
{
    public class CreateCourseDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
