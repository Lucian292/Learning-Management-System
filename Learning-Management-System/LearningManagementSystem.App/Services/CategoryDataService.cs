using LearningManagementSystem.App.Contracts;
using LearningManagementSystem.App.Services.Responses;
using LearningManagementSystem.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LearningManagementSystem.App.Services
{
    public class CategoryDataService : ICategoryDataService
    {
        private const string RequestUri = "api/v1/Categories";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public CategoryDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<CategoryDto>> CreateCategoryAsync(CategoryViewModel categoryViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, categoryViewModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<CategoryDto>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<List<CategoryViewModel>> GetCategoriesAsync()
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
            var categories = JsonSerializer.Deserialize<List<CategoryViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return categories!;
        }

        public async Task<ApiResponse<CategoryDto>> GetCoursesByCategoryAsync(Guid categoryId)
        {

            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            // Construiește URL-ul specific pentru obținerea cursurilor asociate categoriei
            var requestUri = $"{RequestUri}/{categoryId}";

            var result = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var categoryDto = JsonSerializer.Deserialize<CategoryDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new ApiResponse<CategoryDto> { Data = categoryDto, IsSuccess = true };

        }

        public async Task<ApiResponse<CategoryViewModel>> GetCategoryByIdAsync(Guid categoryId)
        {
            httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            // Construiește URL-ul specific pentru obținerea categoriei după Id
            var requestUri = $"{RequestUri}/{categoryId}";

            var result = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var categoryViewModel = JsonSerializer.Deserialize<CategoryViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new ApiResponse<CategoryViewModel> { Data = categoryViewModel, IsSuccess = true };
        }

        public async Task<ApiResponse<CategoryDto>> UpdateCategoryAsync(CategoryViewModel updatedCategoryViewModel)
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
                var updateCategoryDto = new
                {
                    categoryId = updatedCategoryViewModel.CategoryId,
                    updateCategoryDto = new
                    {
                        categoryName = updatedCategoryViewModel.CategoryName,
                        description = updatedCategoryViewModel.Description
                    }
                };

                var result = await httpClient.PutAsJsonAsync(RequestUri, updateCategoryDto);
                result.EnsureSuccessStatusCode();

                var response = await result.Content.ReadFromJsonAsync<ApiResponse<CategoryDto>>();
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
