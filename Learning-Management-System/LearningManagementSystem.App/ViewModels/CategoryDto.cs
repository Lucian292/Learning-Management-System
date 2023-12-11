﻿namespace LearningManagementSystem.App.ViewModels
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<CourseDto>? Courses { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}