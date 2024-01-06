using FluentAssertions;
using LearningManagementSystem.API.Integration.Tests.Base;
using LearningManagementSystem.Application.Features.Chapters.Queries;
using LearningManagementSystem.Application.Features.Questions.Commands.CreateQuestion;
using LearningManagementSystem.Application.Features.Questions.Commands.DeleteQuestion;
using LearningManagementSystem.Application.Features.Questions.Queries.GetQuestionById;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LearningManagementSystem.API.Integration.Tests.Controllers
{
    public class QuestionsControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "api/v1/questions";

        [Fact]
        public async Task When_GetAllQuestionsQueryHandlerIsCalled_Then_Success()
        {
            // Arrange && Act
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.GetAsync(RequestUri);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<QuestionDto>>(responseString);
            // Assert
            result?.Count().Should().Be(4);
        }

        [Fact]
        public async Task When_GetQuestionByIdQueryHandlerIsCalled_Then_Success()
        {
            // Arrange
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Obțineți toate întrebările pentru a obține un ID valid
            var getAllQuestionsResponse = await Client.GetAsync(RequestUri);
            getAllQuestionsResponse.EnsureSuccessStatusCode();
            var getAllQuestionsString = await getAllQuestionsResponse.Content.ReadAsStringAsync();
            var allQuestions = JsonConvert.DeserializeObject<List<QuestionDto>>(getAllQuestionsString);

            // Verificați dacă există cel puțin o întrebare
            if (allQuestions.Any())
            {
                // Alegeți prima întrebare din listă
                var questionId = allQuestions.First().QuestionId;

                // Act
                var getQuestionByIdResponse = await Client.GetAsync($"{RequestUri}/{questionId}");
                getQuestionByIdResponse.EnsureSuccessStatusCode();

                // Assert
                getQuestionByIdResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                var questionByIdString = await getQuestionByIdResponse.Content.ReadAsStringAsync();
                var questionById = JsonConvert.DeserializeObject<QuestionDto>(questionByIdString);
                questionById.Should().NotBeNull();
                questionById.QuestionId.Should().Be(questionId);
            }
            else
            {
                // Acest test necesită cel puțin o întrebare în baza de date pentru a fi executat cu succes
                Assert.True(false, "Nu există întrebări în baza de date pentru a obține o întrebare după ID.");
            }
        }

        [Fact]
        public async Task When_CreateQuestionCommandHandlerIsCalled_Then_Success()
        {
            // Arrange
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var allChaptersResponse = await Client.GetAsync("api/v1/chapters");
            allChaptersResponse.EnsureSuccessStatusCode();
            var allChaptersResponseString = await allChaptersResponse.Content.ReadAsStringAsync();
            var allChapters = JsonConvert.DeserializeObject<List<ChapterDto>>(allChaptersResponseString);

            // Extrage un ID valid pentru un capitol
            var chapterId = allChapters.First().ChapterId;

            var createQuestionCommand = new CreateQuestionCommand
            {
                ChapterId = chapterId,
                Text = "What is the capital of France?"
            };

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, createQuestionCommand);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateQuestionCommandResponse>(responseString);

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Question.Should().NotBeNull();
            result.ValidationsErrors.Should().BeNull(); // No validation errors expected

            result.Question.Text.Should().Be(createQuestionCommand.Text);
            result.Question.ChapterId.Should().Be(createQuestionCommand.ChapterId);
        }

        [Fact]
        public async Task When_DeleteQuestionCommandHandlerIsCalled_Then_Success()
        {
            // Arrange
            string token = CreateTestToken.CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Get all questions to find a valid question ID
            var getAllResponse = await Client.GetAsync(RequestUri);
            getAllResponse.EnsureSuccessStatusCode();
            var getAllResponseString = await getAllResponse.Content.ReadAsStringAsync();
            var allQuestions = JsonConvert.DeserializeObject<List<QuestionDto>>(getAllResponseString);

            // Choose a question ID for deletion
            var questionIdToDelete = allQuestions[0].QuestionId;

            var deleteQuestionCommand = new DeleteQuestionCommand
            {
                QuestionId = questionIdToDelete
            };

            // Act
            var deleteResponse = await Client.DeleteAsync($"{RequestUri}/{questionIdToDelete}");

            // Assert
            deleteResponse.EnsureSuccessStatusCode();
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var deleteResponseString = await deleteResponse.Content.ReadAsStringAsync();
            var deleteResult = JsonConvert.DeserializeObject<DeleteQuestionCommandResponse>(deleteResponseString);

            deleteResult.Should().NotBeNull();
            deleteResult.Success.Should().BeTrue();

            // Verify that the question was actually deleted
            var getDeletedQuestionResponse = await Client.GetAsync($"{RequestUri}/{questionIdToDelete}");
            getDeletedQuestionResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
