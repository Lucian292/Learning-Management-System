using LearningManagementSystem.App.Contracts;
using LearningManagementSystem.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LearningManagementSystem.App.Services
{
    public class QuestionResultDataService : IQuestionResultDataService
    {
        private const string BaseUri = "api/v1/QuestionResults";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public QuestionResultDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<List<QuestionResultDto>> GetQuestionResultByUserId(Guid userId)
        {
            try
            {
                var token = await tokenService.GetTokenAsync();
                if (token is null)
                {
                    throw new ApplicationException("Authentication token is null.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await httpClient.GetAsync($"{BaseUri}/byUser");
                result.EnsureSuccessStatusCode();

                var json = await result.Content.ReadAsStringAsync();

                var response = await result.Content.ReadFromJsonAsync<List<QuestionResultDto>>();

                return response;
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
