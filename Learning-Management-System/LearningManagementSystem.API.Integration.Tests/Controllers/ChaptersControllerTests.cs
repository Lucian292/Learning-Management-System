using FluentAssertions;
using LearningManagementSystem.API.Integration.Tests.Base;
using LearningManagementSystem.Application.Features.Chapters.Commands.CreateChapter;
using LearningManagementSystem.Application.Features.Chapters.Commands.UpdateChapter;
using LearningManagementSystem.Application.Features.Chapters.Queries;
using LearningManagementSystem.Application.Features.Courses.Queries;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace LearningManagementSystem.API.Integration.Tests.Controllers
{
    public class ChaptersControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "api/v1/chapters";

        [Fact]
        public async Task When_GetAllChaptersQueryHandlerIsCalled_Then_Success()
        {
            // Arrange && Act
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.GetAsync(RequestUri);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ChapterDto>>(responseString);
            // Assert
            result?.Count().Should().Be(4);
        }

        [Fact]
        public async Task When_GetByIdChapterQueryHandlerIsCalledWithValidChapterId_Then_ChapterDtoShouldBeReturned()
        {
            // Arrange
            // Obtine toate capitolele
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var allChaptersResponse = await Client.GetAsync(RequestUri);
            allChaptersResponse.EnsureSuccessStatusCode();
            var allChaptersResponseString = await allChaptersResponse.Content.ReadAsStringAsync();
            var allChapters = JsonConvert.DeserializeObject<List<ChapterDto>>(allChaptersResponseString);

            // Extrage un ID valid pentru un capitol
            var chapterId = allChapters.First().ChapterId;

            // Act
            var getByIdChapterResponse = await Client.GetAsync(RequestUri + "/" + chapterId);
            getByIdChapterResponse.EnsureSuccessStatusCode();
            var getByIdChapterResponseString = await getByIdChapterResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ChapterDto>(getByIdChapterResponseString);

            // Assert
            result.Should().NotBeNull();
            result.ChapterId.Should().Be(chapterId);
        }

        [Fact]
        public async Task When_CreateChapterCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
        {
            // Arrange
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Obțineți toate cursurile
            var coursesResponse = await Client.GetAsync("api/v1/Courses");
            coursesResponse.EnsureSuccessStatusCode();
            var coursesResponseString = await coursesResponse.Content.ReadAsStringAsync();
            var courses = JsonConvert.DeserializeObject<List<CourseDto>>(coursesResponseString);

            // Extrageți un ID valid pentru un curs
            var courseId = courses.First().CourseId;

            // Creați comanda pentru crearea capitolului
            var createChapterCommand = new CreateChapterCommand
            {
                Title = "TestChapter",
                CourseId = courseId,
                Link = "TestLink",
            };

            // Act
            var createChapterResponse = await Client.PostAsJsonAsync(RequestUri, createChapterCommand);
            createChapterResponse.EnsureSuccessStatusCode();
            var createChapterResponseString = await createChapterResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateChapterDto>(createChapterResponseString);

            // Assert
            result.Should().NotBeNull();
            result.ChapterId.Should().NotBeEmpty();
            result.CourseId.Should().Be(courseId);
            result.Title.Should().Be(createChapterCommand.Title);
            result.Link.Should().Be(createChapterCommand.Link);
            result.Content.Should().BeEquivalentTo(createChapterCommand.Content);
        }

        [Fact]
        public async Task When_DeleteChapterCommandHandlerIsCalledWithRightParameters_Then_TheChapterShouldBeDeleted()
        {
            // Arrange
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Obțineți toate capitolele
            var chaptersResponse = await Client.GetAsync(RequestUri);
            chaptersResponse.EnsureSuccessStatusCode();
            var chaptersResponseString = await chaptersResponse.Content.ReadAsStringAsync();
            var chapters = JsonConvert.DeserializeObject<List<ChapterDto>>(chaptersResponseString);

            // Verificați dacă există cel puțin un capitol
            if (chapters.Any())
            {
                // Extrageți un ID valid pentru un capitol
                var chapterId = chapters.First().ChapterId;

                // Act
                var deleteChapterResponse = await Client.DeleteAsync(RequestUri + "/" + chapterId);
                deleteChapterResponse.EnsureSuccessStatusCode();

                // Verificați dacă ștergerea a fost reușită
                var getChapterResponse = await Client.GetAsync(RequestUri + "/" + chapterId);
                getChapterResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
            else
            {
                // Acest test necesită cel puțin un capitol în baza de date pentru a fi executat cu succes
                Assert.True(false, "Nu există capitole în baza de date pentru a fi șterse.");
            }
        }

        [Fact]
        public async Task When_UpdateChapterCommandHandlerIsCalledWithRightParameters_Then_TheChapterShouldBeUpdated()
        {
            // Arrange
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Obțineți toate capitolele
            var chaptersResponse = await Client.GetAsync(RequestUri);
            chaptersResponse.EnsureSuccessStatusCode();
            var chaptersResponseString = await chaptersResponse.Content.ReadAsStringAsync();
            var chapters = JsonConvert.DeserializeObject<List<ChapterDto>>(chaptersResponseString);

            // Verificați dacă există cel puțin un capitol
            if (chapters.Any())
            {
                // Extrageți un ID valid pentru un capitol
                var chapterId = chapters.First().ChapterId;

                // Creare comandă pentru actualizarea capitolului
                var updateChapterCommand = new UpdateChapterCommand
                {
                    ChapterId = chapterId,
                    Chapter = new UpdateChapterDto
                    {
                        Title = "UpdatedTitle",
                        Link = "UpdatedLink",
                        Content = Encoding.UTF8.GetBytes("UpdatedContent")
                    }
                };

                // Act
                var updateChapterResponse = await Client.PutAsJsonAsync(RequestUri, updateChapterCommand);
                updateChapterResponse.EnsureSuccessStatusCode();

                // Verificați dacă actualizarea a fost reușită
                var getChapterResponse = await Client.GetAsync(RequestUri + "/" + chapterId);
                getChapterResponse.EnsureSuccessStatusCode();
                var updatedChapter = JsonConvert.DeserializeObject<ChapterDto>(await getChapterResponse.Content.ReadAsStringAsync());

                // Assert
                updatedChapter.Should().NotBeNull();
                updatedChapter.Title.Should().Be(updateChapterCommand.Chapter.Title);
                updatedChapter.Link.Should().Be(updateChapterCommand.Chapter.Link);
                updatedChapter.Content.Should().BeEquivalentTo(updateChapterCommand.Chapter.Content);
            }
            else
            {
                // Acest test necesită cel puțin un capitol în baza de date pentru a fi executat cu succes
                Assert.True(false, "Nu există capitole în baza de date pentru a fi actualizate.");
            }
        }
    }
}
