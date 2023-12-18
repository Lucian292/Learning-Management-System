using LearningManagementSystem.App.Contracts;
using LearningManagementSystem.App.Services.Responses;
using LearningManagementSystem.App.ViewModels;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LearningManagementSystem.App.Services
{
    public class UserDataService : IUserDataService
    {
        private const string RequestUri = "api/UserInfo";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public UserDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<UserDto>> GetUserInfoAsync()
        {
            try
            {
                var token = await tokenService.GetTokenAsync();
                if (token == null)
                {
                    throw new ApplicationException("Authentication token is null.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
                result.EnsureSuccessStatusCode();

                var content = await result.Content.ReadAsStringAsync();
                if (!result.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }

                var userDto = JsonSerializer.Deserialize<UserDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return new ApiResponse<UserDto> { Data = userDto, IsSuccess = true };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
