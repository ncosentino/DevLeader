using Microsoft.Extensions.Options;

using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

Console.WriteLine("Hello World!");

public sealed class OurBusinessLogic
{
    public async Task DoStuffAsync(
        CancellationToken cancellationToken)
    {
        OurApiClient client = new();
        bool loginSuccess = await client.LoginAsync(cancellationToken);
        if (!loginSuccess)
        {
            throw new UnauthorizedAccessException(
                "Could not login to continue doing stuff!");
        }

        Console.WriteLine("Doing stuff after logging in!");
    }
}

public sealed class RosariosBusinessLogic
{
    public async Task DoStuffAsync(
        CancellationToken cancellationToken)
    {
        OurApiClient client = new();
        bool loginSuccess = await client.LoginAsync(cancellationToken);
        if (!loginSuccess)
        {
            throw new UnauthorizedAccessException(
                "Could not login to continue doing stuff!");
        }

        Console.WriteLine("Doing stuff after logging in!");
    }
}

public sealed record LoginInfo(
    string Username,
    string Password);

public sealed class OurApiClient
{
    private const string DefaultUsername = "user";
    private const string DefaultPassword = "pass";
    private const string DefaultLoginApiUrl = "https://ourapi.com/login";

    private static readonly LoginInfo _defaultLogin = new(
        DefaultUsername,
        DefaultPassword);

    private readonly LoginInfo _loginInfo;
    private readonly IProxyHttpClientFactory? _httpClientFactory;

    public OurApiClient() :
        this(_defaultLogin, null)
    {
    }

    public OurApiClient(
        IProxyHttpClientFactory httpClientFactory) :
        this(_defaultLogin, httpClientFactory)
    {
    }

    public OurApiClient(
        LoginInfo loginInfo,
        IProxyHttpClientFactory? httpClientFactory = null)
    {
        _loginInfo = loginInfo;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> LoginAsync(
        CancellationToken cancellationToken)
    {
        var apiUrl = DefaultLoginApiUrl;
        return await LoginAsync(apiUrl, cancellationToken);
    }

    public async Task<bool> LoginAsync(
        string apiUrl,
        CancellationToken cancellationToken)
    {
        Func<string?, HttpContent?, CancellationToken, Task<HttpResponseMessage>> PostAsync =
            _httpClientFactory is not null 
            ? _httpClientFactory.CreateClient().PostAsync 
            : new HttpClient().PostAsync;

        HttpResponseMessage response = await PostAsync(
            apiUrl,
            new StringContent(
                $"{{" +
                $"\"username\":\"{_loginInfo.Username}\"," +
                $"\"password\":\"{_loginInfo.Password}\"" +
                $"}}"),
            cancellationToken);
        return response.IsSuccessStatusCode;
    }
}

public interface IHttpClient
{
    Task<HttpResponseMessage> PostAsync(
        [StringSyntax("Uri")] string? requestUri, 
        HttpContent? content, 
        CancellationToken cancellationToken);
}

public interface IProxyHttpClientFactory
{
    IHttpClient CreateClient();

    IHttpClient CreateClient(string name);
}

public sealed class ProxyFactory : IProxyHttpClientFactory
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProxyFactory(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IHttpClient CreateClient() =>
        CreateClient(Options.DefaultName);

    public IHttpClient CreateClient(string name)
    {
        HttpClient realClient = _httpClientFactory.CreateClient();
        IHttpClient client = new HttpClientWrapper(realClient);
        return client;
    }
}

public sealed class HttpClientWrapper : IHttpClient
{
    private readonly HttpClient _client;

    public HttpClientWrapper(HttpClient client)
    {
        _client = client;
    }

    public Task<HttpResponseMessage> PostAsync(
        [StringSyntax("Uri")] string? requestUri,
        HttpContent? content,
        CancellationToken cancellationToken) =>
        _client.PostAsync(requestUri, content, cancellationToken);
}