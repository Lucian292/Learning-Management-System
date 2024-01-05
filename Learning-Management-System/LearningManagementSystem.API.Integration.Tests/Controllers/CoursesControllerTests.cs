﻿using FluentAssertions;
using LearningManagementSystem.API.Integration.Tests.Base;
using LearningManagementSystem.Application.Features.Categories.Commands.CreateCategory;
using LearningManagementSystem.Application.Features.Couerses.Commands.CreateCourse;
using LearningManagementSystem.Application.Features.Courses.Queries;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LearningManagementSystem.API.Integration.Tests.Controllers
{
    public class CoursesControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "api/v1/Courses";

        [Fact]
        public async Task When_GetAllCoursesQueryHandlerIsCalled_Then_Success()
        {
            // Arrange && Act
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.GetAsync(RequestUri);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<CourseDto>>(responseString);
            // Assert
            result?.Count().Should().Be(4);
        }

        [Fact]
        public async Task When_CreateCourseCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
        {
            // Arrange
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Create a category first
            var createCategoryCommand = new CreateCategoryCommand
            {
                CategoryName = "TestCategory",
                Description = "TestCategoryDescription"
            };

            var createCategoryResponse = await Client.PostAsJsonAsync("api/v1/categories", createCategoryCommand);
            createCategoryResponse.EnsureSuccessStatusCode();
            var createCategoryResponseString = await createCategoryResponse.Content.ReadAsStringAsync();
            var createdCategory = JsonConvert.DeserializeObject<CreateCategoryDto>(createCategoryResponseString);

            // Create a course command
            var createCourseCommand = new CreateCourseCommand
            {
                Title = "TestCourse",
                Description = "TestCourseDescription",
                CategoryId = createdCategory.CategoryId
            };

            // Act
            var createCourseResponse = await Client.PostAsJsonAsync(RequestUri, createCourseCommand);
            createCourseResponse.EnsureSuccessStatusCode();
            var createCourseResponseString = await createCourseResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateCourseDto>(createCourseResponseString);

            // Assert
            result?.Should().NotBeNull();
            result?.Title.Should().Be(createCourseCommand.Title);
            result?.Description.Should().Be(createCourseCommand.Description);
            result?.CategoryId.Should().Be(createCourseCommand.CategoryId);
            result?.UserId.Should().NotBeEmpty();
            result?.CourseId.Should().NotBeEmpty();
        }
    }
}