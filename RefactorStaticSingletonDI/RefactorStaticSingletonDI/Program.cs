MyBusinessLogic businessLogic = new();
var result = await businessLogic.IsServiceDataValidAsync(
    CancellationToken.None);
Console.WriteLine($"Result: {result}");

public static class Globals
{
    public static string UserName = "Dev Leader";

    public static string ApiKey = "133742069";

    public static int ThisIsCurrentlyUnused = 42;
}

public sealed class ApiClientConfigSingleton
{
    private static readonly Lazy<IApiClientConfig> _lazyInstance = new(
        () => new ApiClientConfig());

    public static IApiClientConfig Instance => _lazyInstance.Value;

    private sealed class ApiClientConfig : IApiClientConfig
    {
        public string UserName => Globals.UserName;

        public string ApiKey => Globals.ApiKey;
    }
}

public interface IApiClientConfig
{
    string UserName { get; }

    string ApiKey { get; }
}

public sealed class MyBusinessLogic
{
    private readonly MyApiClient _apiClient;

    public MyBusinessLogic()
    {
        _apiClient = new();
    }

    public async Task<bool> IsServiceDataValidAsync(
        CancellationToken cancellationToken)
    {
        var resultFromService = await _apiClient.FetchMetadataAsync(
            cancellationToken);

        if (string.Equals(
            resultFromService,
            "valid",
            StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (string.Equals(
            resultFromService,
            "invalid",
            StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        throw new FormatException(
            $"Unexpected result from service: {resultFromService}");
    }
}

public sealed class MyApiClient
{
    public async Task<string> FetchMetadataAsync(
        CancellationToken cancellationToken)
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Add(
            "X-Api-Key", 
            ApiClientConfigSingleton.Instance.ApiKey);
        client.DefaultRequestHeaders.Add(
            "X-Username",
            ApiClientConfigSingleton.Instance.UserName);
        return await client.GetStringAsync(
            "https://www.devleader.ca/data?metakey=test",
            cancellationToken);
    }
}

public sealed class SomeOtherCodeThatUsesTheSingleton
{
    public void ThisMethodDoesntReallyMatter()
    {
        Console.WriteLine($"Username: {ApiClientConfigSingleton.Instance.UserName}");
        Console.WriteLine($"ApiKey: {ApiClientConfigSingleton.ApiKey}");
    }
}