using FluentAssertions;
using LearningManagementSystem.Domain.Entities.Courses;

namespace LearningManagementSystem.Domain.Tests
{
    public class ChapterTests
    {
        [Fact]
        public void When_CreateChapterIsCalled_And_ParametersAreValid_Then_SuccessIsReturned()
        {
            // Arrange
            Guid courseId = Guid.Parse("b5ac2a67-497e-48a5-9242-76cae177a1d3");
            string title = "Chapter Test Title";

            // Act
            var result = Chapter.Create(courseId, title);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Theory]
        [InlineData("emptyGuid", "Test Title", "Course Id is required")]
        [InlineData("b5ac2a67-497e-48a5-9242-76cae177a1d3", " ", "Title is required")]
        public void When_CreateChapterIsCalled_And_ParametersAreInvalid_Then_FailureIsReturned(string courseId, string title, string expectedErrorMessage)
        {
            // Arrange
            Guid courseIdAsGuid = Guid.Empty;

            if (!string.IsNullOrWhiteSpace(courseId) && Guid.TryParse(courseId, out Guid courseGuid))
            {
                courseIdAsGuid = courseGuid;
            }

            // Act
            var result = Chapter.Create(courseIdAsGuid, title);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain(expectedErrorMessage);
        }

        [Fact]
        public void When_AttachContentIsCalled_And_ContentIsNotNull_Then_ContentIsAttachedToChapter()
        {
            // Arrange
            Guid courseId = Guid.Parse("b5ac2a67-497e-48a5-9242-76cae177a1d3");
            string title = "Chapter Test Title";
            var chapter = Chapter.Create(courseId, title).Value;
            var content = new byte[] { 1, 2, 3 };

            // Act
            chapter.AttachContent(content);

            // Assert
            Assert.Equal(content, chapter.Content);
        }

        [Fact]
        public void When_AttachContentIsCalled_And_ContentIsNotNull_Then_ContentRemainsUnchanged()
        {
            // Arrange
            Guid courseId = Guid.Parse("b5ac2a67-497e-48a5-9242-76cae177a1d3");
            string title = "Chapter Test Title";
            var chapter = Chapter.Create(courseId, title).Value;
            var content = chapter.Content;

            // Act
            chapter.AttachContent(null!);

            // Assert
            Assert.Equal(content, chapter.Content);
        }

        [Fact]
        public void When_AttachLinkIsCalled_And_LinkIsValid_Then_LinkIsAttachedToChapter()
        {
            // Arrange
            Guid courseId = Guid.Parse("b5ac2a67-497e-48a5-9242-76cae177a1d3");
            string title = "Chapter Test Title";
            var chapter = Chapter.Create(courseId, title).Value;
            var link = "google.com";

            // Act
            chapter.AttachLink(link);

            // Assert
            chapter.Link.Should().Be(link);
        }

        [Fact]
        public void When_AttachLinkIsCalled_And_LinkIsInvalid_Then_LinkRemainsUnchanged()
        {
            // Arrange
            Guid courseId = Guid.Parse("b5ac2a67-497e-48a5-9242-76cae177a1d3");
            string title = "Chapter Test Title";
            var chapter = Chapter.Create(courseId, title).Value;
            var link = chapter.Link;

            // Act
            chapter.AttachLink(null!);

            // Assert
            chapter.Link.Should().Be(link);
        }

        [Fact]
        public void When_UpdateChapterIsCalled_Then_ChapterPropertiesAreUpdated()
        {
            // Arrange
            Guid courseId = Guid.Parse("b5ac2a67-497e-48a5-9242-76cae177a1d3");
            string title = "Chapter Test Title";
            var chapter = Chapter.Create(courseId, title).Value;
            var link = "google.com";
            string newTitle = "Updated Chapter Title";

            // Act
            chapter.Update(newTitle, link);

            // Assert
            chapter.Title.Should().Be(newTitle);
            chapter.Link.Should().Be(link);
        }
    }
}
