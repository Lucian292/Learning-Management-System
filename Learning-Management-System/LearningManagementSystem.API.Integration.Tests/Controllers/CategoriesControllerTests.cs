using FluentAssertions;
using LearningManagementSystem.API.Integration.Tests.Base;
using LearningManagementSystem.Application.Features.Categories;
using LearningManagementSystem.Application.Features.Categories.Commands.CreateCategory;
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
            string token = CreateToken();
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
            string token = CreateToken();
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

        private string CreateToken()
        {
            return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(
            new JwtSecurityToken(
                issuer: JwtTokenProvider.Issuer,
                audience: JwtTokenProvider.Issuer,
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Name, "Test"),
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                },
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: JwtTokenProvider.SigningCredentials
            ));
        }

    }
}
