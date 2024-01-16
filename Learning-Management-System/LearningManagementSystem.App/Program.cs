using Blazored.LocalStorage;
using FluentAssertions.Common;
using LearningManagementSystem.App;
using LearningManagementSystem.App.Auth;
using LearningManagementSystem.App.Contracts;
using LearningManagementSystem.App.Services;
using LearningManagementSystem.App.SharedDataServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http.Features;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    config.JsonSerializerOptions.WriteIndented = false;
});
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ChapterQuizShare>();
builder.Services.AddScoped<CustomStateProvider>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<RoleAuthorizationService>();
builder.Services.AddHttpClient<ICategoryDataService, CategoryDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7190/");
});
builder.Services.AddHttpClient<ICourseDataService, CourseDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7190/");
});
builder.Services.AddHttpClient<IQuizDataService, QuizDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7190/");
});
builder.Services.AddHttpClient<IChapterDataService, ChapterDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7190/");
});
builder.Services.AddHttpClient<IEnrollmentDataService, EnrollmentDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7190/");
});
builder.Services.AddHttpClient<IUserDataService, UserDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7190/");
});
builder.Services.AddHttpClient<IQuestionResultDataService, QuestionResultDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7190/");
});
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7190/");
});
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 15728640; // 15 MB
});
await builder.Build().RunAsync();

