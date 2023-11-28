using Moq;

using Xunit;

namespace LegacyCodeNightmare.Tests
{
    public class Tests
    {
        [Fact]
        public async Task GetSomeBlogArticleAsync_HttpAndFileWriteSuccess_FileMatches()
        {
            MockRepository mockRepository = new(MockBehavior.Strict);
            var httpClient = mockRepository.Create<IHttpClient>();

            httpClient
                .Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync("some html");

            BlogFetcher blogFetcher = new(httpClient.Object);
            await blogFetcher.GetSomeBlogArticleAsync();

            Assert.True(File.Exists("blog.html"));
            var result = await File.ReadAllTextAsync("blog.html");
            Assert.Equal("some html", result);

            mockRepository.VerifyAll();
        }
    }
}