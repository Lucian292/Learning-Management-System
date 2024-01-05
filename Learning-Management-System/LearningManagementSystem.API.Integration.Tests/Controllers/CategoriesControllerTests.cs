using FluentAssertions;
using LearningManagementSystem.API.Integration.Tests.Base;
using LearningManagementSystem.Application.Features.Categories;
using LearningManagementSystem.Application.Features.Categories.Commands.CreateCategory;
using LearningManagementSystem.Application.Features.Categories.Commands.DeleteCategory;
using LearningManagementSystem.Application.Features.Categories.Commands.UpdateCategory;
using LearningManagementSystem.Application.Features.Categories.Queries.GetById;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace LearningManagementSystem.API.Integration.Tests.Controllers
{
    public class CategoriesControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "api/v1/categories";

        [Fact]
        public async Task When_GetAllCategoriesQueryHandlerIsCalled_Then_Success()
        {
            // Arrange && Act
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.GetAsync(RequestUri);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<CategoryDto>>(responseString);
            // Assert
            result?.Count().Should().Be(4);
        }

        [Fact]
        public async Task When_PostCategoryCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
        {
            // Arrange
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var command = new CreateCategoryCommand
            {
                CategoryName = "Test",
                Description = "Test"
            };
            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, command);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateCategoryDto>(responseString);
            // Assert
            result?.Should().NotBeNull();
            result?.CategoryName.Should().Be(command.CategoryName);
            result?.Description.Should().Be(command.Description);
        }

        [Fact]
        public async Task When_UpdateCategoryCommandHandlerIsCalledWithRightParameters_Then_TheEntityUpdatedShouldBeReturned()
        {
            // Arrange
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Create a category first
            var createCommand = new CreateCategoryCommand
            {
                CategoryName = "InitialCategory",
                Description = "InitialDescription"
            };

            var createResponse = await Client.PostAsJsonAsync(RequestUri, createCommand);
            createResponse.EnsureSuccessStatusCode();
            var createResponseString = await createResponse.Content.ReadAsStringAsync();
            var createdCategory = JsonConvert.DeserializeObject<CreateCategoryDto>(createResponseString);

            // Replace the CategoryId with the created category's Id
            var categoryId = createdCategory.CategoryId;

            var updateCommand = new UpdateCategoryCommand
            {
                CategoryId = categoryId,
                UpdateCategoryDto = new UpdateCategoryDto
                {
                    CategoryName = "UpdatedCategory",
                    Description = "UpdatedDescription"
                }
            };

            // Act
            var response = await Client.PutAsJsonAsync(RequestUri, updateCommand);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UpdateCategoryDto>(responseString);

            // Assert
            result?.Should().NotBeNull();
            result?.CategoryName.Should().Be(updateCommand.UpdateCategoryDto.CategoryName);
            result?.Description.Should().Be(updateCommand.UpdateCategoryDto.Description);
        }

        [Fact]
        public async Task When_DeleteCategoryCommandHandlerIsCalledWithValidCategoryId_Then_SuccessShouldBeTrue()
        {
            // Arrange
            // Assuming you have a category already created with a valid CategoryId
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Create a category first
            var createCommand = new CreateCategoryCommand
            {
                CategoryName = "TestCategoryToDelete",
                Description = "TestCategoryToDeleteDescription"
            };

            var createResponse = await Client.PostAsJsonAsync(RequestUri, createCommand);
            createResponse.EnsureSuccessStatusCode();
            var createResponseString = await createResponse.Content.ReadAsStringAsync();
            var createdCategory = JsonConvert.DeserializeObject<CreateCategoryDto>(createResponseString);

            var deleteCommand = new DeleteCategoryCommand
            {
                CategoryId = createdCategory.CategoryId
            };

            // Act
            var response = await Client.DeleteAsync($"{RequestUri}/{deleteCommand.CategoryId}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DeleteCategoryCommandResponse>(responseString);

            // Assert
            result?.Should().NotBeNull();
            result?.Success.Should().BeTrue();
        }

        [Fact]
        public async Task When_GetByIdCategoryHandlerIsCalledWithValidCategoryId_Then_CategoryDtoShouldBeReturned()
        {
            // Arrange
            // Assuming you have a category already created with a valid CategoryId
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Create a category first
            var createCommand = new CreateCategoryCommand
            {
                CategoryName = "TestCategoryToGetById",
                Description = "TestCategoryToGetByIdDescription"
            };

            var createResponse = await Client.PostAsJsonAsync(RequestUri, createCommand);
            createResponse.EnsureSuccessStatusCode();
            var createResponseString = await createResponse.Content.ReadAsStringAsync();
            var createdCategory = JsonConvert.DeserializeObject<CreateCategoryDto>(createResponseString);

            // Act
            var response = await Client.GetAsync($"{RequestUri}/{createdCategory.CategoryId}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetSingleCategoryDto>(responseString);

            // Assert
            result?.Should().NotBeNull();
            result?.CategoryId.Should().Be(createdCategory.CategoryId);
            result?.CategoryName.Should().Be(createdCategory.CategoryName);
            result?.Description.Should().Be(createdCategory.Description);
            result?.Courses.Should().NotBeNull();
            result?.Courses.Should().BeEmpty(); // Assuming no courses are associated with the category for this test
        }

    }
}
