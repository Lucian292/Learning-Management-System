using LearningManagementSystem.App.Contracts;
using LearningManagementSystem.App.Services.Responses;
using LearningManagementSystem.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LearningManagementSystem.App.Services
{
    public class CourseDataService : ICourseDataService
    {
        private const string RequestUri = "api/v1/Courses";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public CourseDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<CourseDto>> CreateCourseAsync(CourseViewModel courseViewModel)
        {
            try
            {
                var token = await tokenService.GetTokenAsync();
                if (token == null)
                {
                    // Gestionare scenariu în care token-ul este null
                    throw new ApplicationException("Authentication token is null.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await httpClient.PostAsJsonAsync(RequestUri, courseViewModel);
                result.EnsureSuccessStatusCode();

                var response = await result.Content.ReadFromJsonAsync<ApiResponse<CourseDto>>();
                if (response != null)
                {
                    response.IsSuccess = result.IsSuccessStatusCode;
                    return response;
                }

                // Gestionare scenariu în care răspunsul JSON este null
                throw new ApplicationException("Failed to deserialize the response JSON.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                // Gestionare și înregistrare excepție
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                // Gestionare și înregistrare excepție
                throw;
            }
        }



        public async Task<List<CourseDto>> GetCoursesAsync()
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var courses = JsonSerializer.Deserialize<List<CourseDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return courses!;
        }

        public async Task<ApiResponse<CourseViewModel>> GetCourseByIdAsync(Guid courseId)
        {
            httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            // Construiește URL-ul specific pentru obținerea cursurilor asociate categoriei
            var requestUri = $"{RequestUri}/{courseId}";

            var result = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var courseViewModel = JsonSerializer.Deserialize<CourseViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new ApiResponse<CourseViewModel> { Data = courseViewModel, IsSuccess = true };
        }

        public async Task<ApiResponse<CourseDto>> GetChaptersByCourseAsync(Guid courseId)
        {
            httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            // Construiește URL-ul specific pentru obținerea cursurilor asociate categoriei
            var requestUri = $"{RequestUri}/{courseId}";

            var result = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var courseDto = JsonSerializer.Deserialize<CourseDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new ApiResponse<CourseDto> { Data = courseDto, IsSuccess = true };
        }

        public async Task<ApiResponse<CourseDto>> UpdateCourseAsync(CourseViewModel updatedCourseViewModel)
        {
            try
            {
                var token = await tokenService.GetTokenAsync();
                if (token == null)
                {
                    // Gestionare scenariu în care token-ul este null
                    throw new ApplicationException("Authentication token is null.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var updateCourseDto = new
                {
                    courseId = updatedCourseViewModel.CourseId,
                    updateCourseDto = new
                    {
                        title = updatedCourseViewModel.Title,
                        description = updatedCourseViewModel.Description
                    }
                };

                var result = await httpClient.PutAsJsonAsync(RequestUri, updateCourseDto);
                result.EnsureSuccessStatusCode();

                var response = await result.Content.ReadFromJsonAsync<ApiResponse<CourseDto>>();
                if (response != null)
                {
                    response.IsSuccess = result.IsSuccessStatusCode;
                    return response;
                }

                // Gestionare scenariu în care răspunsul JSON este null
                throw new ApplicationException("Failed to deserialize the response JSON.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                // Gestionare și înregistrare excepție
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                // Gestionare și înregistrare excepție
                throw;
            }
        }

    }
}
