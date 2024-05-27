using SomeThirdPartyAssembly;

Console.WriteLine("Hello, World!");
NicksSystem nicksSystem = new NicksSystem(
    new NicksConfig("DevLeader", "password"),
    new CoolClient(),
    null);
try
{
    await nicksSystem.DoNiceToHaveWorkWithAuth(CancellationToken.None);
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

public interface ILogger
{
    void LogError(Exception ex, string message);
}

public sealed record NicksConfig(
    string Username,
    string Password);

public sealed class NicksSystem(
    NicksConfig _nicksConfig,
    ICoolClient _coolClient,
    ILogger _logger)
{
    public async Task DoNiceToHaveWorkWithAuth(
        CancellationToken cancellationToken)
    {
        string authToken = null;
        try
        {
            authToken = await
                GetAuthTokenAsync(cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return;
        }

        // TODO: do something with the token
    }

    public async Task DoCriticalWorkWithAuth(
        CancellationToken cancellationToken)
    {
        var authToken = await
                GetAuthTokenAsync(cancellationToken)
                .ConfigureAwait(false);
        // TODO: do something with the token
    }

    private async Task<string> GetAuthTokenAsync(
        CancellationToken cancellationToken)
    {
        var retries = 3;
        for (int i = 0; i < retries; i++)
        {
            try
            {
                var authToken = await _coolClient
                    .LoginAsync(_nicksConfig.Username, _nicksConfig.Password, cancellationToken)
                    .ConfigureAwait(false);
                return authToken;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                // TODO: handle this the way you need to for your purposes!
                _logger.LogError(
                    ex,
                    "Failed to get auth token.");
            }
        }

        throw new InvalidOperationException(
            $"Failed to get auth token after {retries} retries.");
    }
}