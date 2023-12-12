﻿using MediatR;
using System.Globalization;

namespace LearningManagementSystem.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<CreateCategoryCommandResponse>
    {
        public string CategoryName { get; set; } = default!;

        public string? Description { get; set; }
    }
}
