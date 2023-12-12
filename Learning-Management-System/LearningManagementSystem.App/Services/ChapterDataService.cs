using LearningManagementSystem.App.Contracts;
using LearningManagementSystem.App.Services.Responses;
using LearningManagementSystem.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LearningManagementSystem.App.Services
{
    public class ChapterDataService : IChapterDataService
    {
        private const string RequestUri = "api/v1/chapters";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ChapterDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<ChapterDto>> CreateChapterAsync(ChapterViewModel chapterViewModel)
        {
            try
            {
                var token = await tokenService.GetTokenAsync();
                if (token == null)
                {
                    throw new ApplicationException("Authentication token is null.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await httpClient.PostAsJsonAsync(RequestUri, chapterViewModel);
                result.EnsureSuccessStatusCode();

                var response = await result.Content.ReadFromJsonAsync<ApiResponse<ChapterDto>>();
                if (response != null)
                {
                    response.IsSuccess = result.IsSuccessStatusCode;
                    return response;
                }

                throw new ApplicationException("Failed to deserialize the response JSON.");
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

        public async Task<List<ChapterDto>> GetChaptersAsync()
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
            var chapters = JsonSerializer.Deserialize<List<ChapterDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return chapters!;
        }

        public async Task<ApiResponse<ChapterDto>> UpdateChapterAsync(ChapterViewModel updatedChapterViewModel)
        {
            try
            {
                var token = await tokenService.GetTokenAsync();
                if (token == null)
                {
                    throw new ApplicationException("Authentication token is null.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var updateChapterDto = new
                {
                    ChapterId = updatedChapterViewModel.ChapterId,
                    Chapter = new
                    {
                        Title = updatedChapterViewModel.Title,
                        Link = updatedChapterViewModel.Link,
                        Content = updatedChapterViewModel.Content
                    }
                };

                var result = await httpClient.PutAsJsonAsync(RequestUri, updateChapterDto);
                result.EnsureSuccessStatusCode();

                var response = await result.Content.ReadFromJsonAsync<ApiResponse<ChapterDto>>();
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

        public async Task<ApiResponse<ChapterViewModel>> GetChapterByIdAsync(Guid chapterId)
        {
            httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            // Construiește URL-ul specific pentru obținerea categoriei după Id
            var requestUri = $"{RequestUri}/{chapterId}";

            var result = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var chapterViewModel = JsonSerializer.Deserialize<ChapterViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new ApiResponse<ChapterViewModel> { Data = chapterViewModel, IsSuccess = true };
        }
    }
}
