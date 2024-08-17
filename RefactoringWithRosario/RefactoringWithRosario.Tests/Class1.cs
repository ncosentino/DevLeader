using Moq;

using Xunit;

namespace RefactoringWithRosario.Tests;

public class Class1
{
    private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Strict);

    [Fact]
    public void Original()
    {
        OurApiClient client = new();

        client.LoginAsync(CancellationToken.None).Wait();

        // some assertions etc...
    }

    [Fact]
    public void WithHttpClientFactory()
    {
        var proxyFactory = _mockRepository.Create<IProxyHttpClientFactory>();
        OurApiClient client = new(proxyFactory.Object);

        client.LoginAsync(CancellationToken.None).Wait();

        // some assertions etc...
    }

    [Fact]
    public async Task WithHttpClientFactory_And_UsernameAndPassword()
    {
        var proxyFactory = _mockRepository.Create<IProxyHttpClientFactory>();
        OurApiClient client = new(
            new LoginInfo(
                "Nick",
                "Password123"),
            proxyFactory.Object);

        await client.LoginAsync(
            "Some other url",
            CancellationToken.None);

        // some assertions etc...
    }

    private sealed class FakeHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            throw new NotImplementedException();
        }
    }
}
