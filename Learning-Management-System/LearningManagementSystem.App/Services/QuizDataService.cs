using LearningManagementSystem.App.Contracts;
using LearningManagementSystem.App.Services.Responses;
using LearningManagementSystem.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LearningManagementSystem.App.Services
{
    public class QuizDataService : IQuizDataService
    {
        private const string RequestUri = "api/v1/chapters";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public QuizDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }
        public async Task<ApiResponse<QuizDto>> CreateQuizAsync(CreateQuizViewModel quizViewModel)
        {
            try
            {
                var token = await tokenService.GetTokenAsync();
                if (token == null)
                {
                    throw new ApplicationException("Authentication token is null.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await httpClient.PostAsJsonAsync(RequestUri + "/create-quiz", quizViewModel);
                result.EnsureSuccessStatusCode();

                var response = await result.Content.ReadFromJsonAsync<ApiResponse<QuizDto>>();
                if (response != null)
                {
                    response.IsSuccess = result.IsSuccessStatusCode;
                    return response;
                }

                return new ApiResponse<QuizDto>
                {
                    Message = "Failed to deserialize the response JSON.",
                    ValidationErrors = "Failed to deserialize the response JSON.",
                    IsSuccess = false
                };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                return new ApiResponse<QuizDto>
                {
                    Message = ex.Message,
                    ValidationErrors = $"HTTP request failed: {ex.Message}",
                    IsSuccess = false
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return new ApiResponse<QuizDto>
                {
                    Message = ex.Message,
                    ValidationErrors = $"An unexpected error occurred: {ex.Message}",
                    IsSuccess = false
                };
            }
        }
    }
}
