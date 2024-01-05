using FluentAssertions;
using LearningManagementSystem.API.Integration.Tests.Base;
using LearningManagementSystem.Application.Features.Categories;
using LearningManagementSystem.Application.Features.Categories.Commands.CreateCategory;
using LearningManagementSystem.Application.Features.Couerses.Commands.CreateCourse;
using LearningManagementSystem.Application.Features.Courses.Commands.UpdateCourse;
using LearningManagementSystem.Application.Features.Courses.Commands.UpdateCourseCommand;
using LearningManagementSystem.Application.Features.Courses.Queries;
using LearningManagementSystem.Application.Features.Courses.Queries.GetByProfessorId;
using Newtonsoft.Json;
using System.Net;
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

            //obtain categories from database first
            var categoriesResponse = await Client.GetAsync("api/v1/Categories");
            categoriesResponse.EnsureSuccessStatusCode();
            var categoriesResponseString = await categoriesResponse.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<CategoryDto>>(categoriesResponseString);

            //extract id from first category
            var categoryId = categories.First().CategoryId;

            // Create a course command
            var createCourseCommand = new CreateCourseCommand
            {
                Title = "TestCourse",
                Description = "TestCourseDescription",
                CategoryId = categoryId
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

        [Fact]
        public async Task When_CreateCourseCommandHandlerIsCalledWithInvalidParameters_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Create a course command with invalid parameters (empty title and invalid category ID)
            var invalidCreateCourseCommand = new CreateCourseCommand
            {
                Title = string.Empty,  // Empty title
                Description = "TestCourseDescription",
                CategoryId = Guid.NewGuid()  // Invalid category ID
            };

            // Act
            var createCourseResponse = await Client.PostAsJsonAsync(RequestUri, invalidCreateCourseCommand);

            // Assert
            createCourseResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task When_UpdateCourseCommandHandlerIsCalledWithRightParameters_Then_TheEntityUpdatedShouldBeReturned()
        {
            // Arrange
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Obtain courses from the database first
            var coursesResponse = await Client.GetAsync(RequestUri);
            coursesResponse.EnsureSuccessStatusCode();
            var coursesResponseString = await coursesResponse.Content.ReadAsStringAsync();
            var courses = JsonConvert.DeserializeObject<List<CourseDto>>(coursesResponseString);

            // Extract ID from the first course (assuming at least one course exists)
            var courseId = courses.First().CourseId;

            // Create an update course command
            var updateCourseCommand = new UpdateCourseCommand
            {
                CourseId = courseId,
                UpdateCourseDto = new UpdateCourseDto
                {
                    Title = "UpdatedTestCourse",
                    Description = "UpdatedTestCourseDescription"
                }
            };

            // Act
            var updateCourseResponse = await Client.PutAsJsonAsync(RequestUri, updateCourseCommand);
            updateCourseResponse.EnsureSuccessStatusCode();
            var updateCourseResponseString = await updateCourseResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UpdateCourseDto>(updateCourseResponseString);

            // Assert
            result?.Should().NotBeNull();
            result?.Title.Should().Be(updateCourseCommand.UpdateCourseDto.Title);
            result?.Description.Should().Be(updateCourseCommand.UpdateCourseDto.Description);
        }

        [Fact]
        public async Task When_GetByIdCourseQueryHandlerIsCalledWithValidCourseId_Then_CourseDtoShouldBeReturned()
        {
            // Arrange
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Obtain courses from the database first
            var coursesResponse = await Client.GetAsync(RequestUri);
            coursesResponse.EnsureSuccessStatusCode();
            var coursesResponseString = await coursesResponse.Content.ReadAsStringAsync();
            var courses = JsonConvert.DeserializeObject<List<CourseDto>>(coursesResponseString);

            // Extract ID from the first course (assuming at least one course exists)
            var courseId = courses.First().CourseId;

            // Act
            var getByIdResponse = await Client.GetAsync(RequestUri + "/" + courseId);
            getByIdResponse.EnsureSuccessStatusCode();
            var getByIdResponseString = await getByIdResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CourseDto>(getByIdResponseString);

            // Assert
            result?.Should().NotBeNull();
            result?.CourseId.Should().Be(courseId);
            result?.Title.Should().NotBeNullOrEmpty();
            result?.Description.Should().NotBeNullOrEmpty();
            result?.UserId.Should().NotBeEmpty();
            result?.CategoryId.Should().NotBeEmpty();
            result?.Chapters.Should().NotBeNull();
        }

        [Fact]
        public async Task When_GetProfessorCoursesEndpointIsCalledWithValidProfessorId_Then_CoursesListShouldBeReturned()
        {
            // Arrange
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var getProfessorCoursesResponse = await Client.GetAsync(RequestUri + "/ByProfessor");
            getProfessorCoursesResponse.EnsureSuccessStatusCode();
            var getProfessorCoursesResponseString = await getProfessorCoursesResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetCoursesByProfessorIdQueryResponse>(getProfessorCoursesResponseString);

            // Assert
            result?.Should().NotBeNull();
            result?.Success.Should().BeTrue();
            result?.Courses.Should().NotBeNull();

            // Validate the structure of the returned data
            foreach (var course in result?.Courses)
            {
                course?.CourseId.Should().NotBeEmpty();
                course?.Title.Should().NotBeNullOrEmpty();
                course?.Description.Should().NotBeNullOrEmpty();
                course?.UserId.Should().NotBeEmpty();
                course?.CategoryId.Should().NotBeEmpty();
                course?.Chapters.Should().NotBeNull(); // Assuming the course has associated chapters
            }
        }

    }
}
