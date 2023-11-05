// See https://aka.ms/new-console-template for more information

using LearningManagementSystem.Infrastructure.Data;

Console.WriteLine("Hello, World!");

LearningManagementSystemDbContext context = new();
Console.WriteLine(context); // 0