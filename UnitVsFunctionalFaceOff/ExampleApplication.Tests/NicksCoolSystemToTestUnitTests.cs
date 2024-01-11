using Moq;

using Xunit;

namespace ExampleApplication.Tests;

public class NicksCoolSystemToTestUnitTests
{
    [Fact]
    private async Task GetTitlesAsync_ReturnsExpectedTitles()
    {
        // Arrange
        MockRepository mockRepository = new(MockBehavior.Strict);
        var mockHttpClientFactory = mockRepository
            .Create<IHttpClientFactory>();
        var mockHttpClient = mockRepository
            .Create<IHttpClient>();

        mockHttpClientFactory
            .Setup(x => x.CreateClient("devleader"))
            .Returns(mockHttpClient.Object);

        mockHttpClient
            .Setup(x => x.GetStringAsync("https://devleader.ca/feed/"))
            .ReturnsAsync(
                "GARBAGE TEXT HERE" +
                "<title>Title 1</title>" +
                "GARBAGE TEXT HERE" +
                "<title>Title 2</title>" +
                "GARBAGE TEXT HERE" +
                "<title>What New Developers Need To Know</title>" +
                "GARBAGE TEXT HERE");
        mockHttpClient
            .Setup(x => x.Dispose());

        var systemToTest = new NicksCoolSystemToTest(
            new DevLeaderFeedFetcher(mockHttpClientFactory.Object),
            new HtmlElementFinder());

        // Act
        var titles = await systemToTest.GetTitlesAsync();

        // Assert
        Assert.Equal(3, titles.Count);
        Assert.Equal("Title 1", titles[0]);
        Assert.Equal("Title 2", titles[1]);
        Assert.Equal(
            "What New Developers Need To Know", 
            titles[2]);

        mockRepository.VerifyAll();
    }
}
