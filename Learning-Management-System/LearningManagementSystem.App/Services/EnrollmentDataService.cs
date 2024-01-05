using LearningManagementSystem.App.Contracts;
using LearningManagementSystem.App.Services.Responses;
using LearningManagementSystem.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace LearningManagementSystem.App.Services
{
    public class EnrollmentDataService : IEnrollmentDataService
    {
        private const string RequestUri = "api/v1/Enrollment";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public EnrollmentDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<List<EnrolledCourseDto>> GetEnrolledCoursesAsync(Guid userId)
        {
            try
            {
                var token = await tokenService.GetTokenAsync();
                if (token == null)
                {
                    throw new ApplicationException("Authentication token is null.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await httpClient.GetAsync($"{RequestUri}/byUser");
                result.EnsureSuccessStatusCode();

                var json = await result.Content.ReadAsStringAsync();

                var response = await result.Content.ReadFromJsonAsync<List<EnrolledCourseDto>>();
                return response ?? new List<EnrolledCourseDto>();
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

        public async Task<EnrolledCourseDto> CreateEnrollmentAsync(EnrolledCourseDto enrollment)
        {
            try
            {
                var token = await tokenService.GetTokenAsync();
                if (token == null)
                {
                    throw new ApplicationException("Authentication token is null.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var json = JsonSerializer.Serialize(enrollment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await httpClient.PostAsync(RequestUri, content);
                result.EnsureSuccessStatusCode();

                var responseJson = await result.Content.ReadAsStringAsync();
                var response = JsonSerializer.Deserialize<EnrolledCourseDto>(responseJson);
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
