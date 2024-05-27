using System.Net;

namespace SomeThirdPartyAssembly;

public interface ICoolClient
{
    /// <summary>
    /// Authorizes the user with the provided credentials.
    /// </summary>
    /// <param name="username">
    /// The username of the user to authorize.
    /// </param>
    /// <param name="password">
    /// The password of the user to authorize.
    /// </param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// Returns a string representing the authorization token.
    /// </returns>
    /// <exception cref="UnauthorizedAccessException">
    /// Thrown when the provided credentials are invalid.
    /// </exception>
    Task<string> LoginAsync(
        string username, 
        string password,
        CancellationToken cancellationToken);
}

public sealed class CoolClient : ICoolClient
{
    /// <inheritdoc/>
    public async Task<string> LoginAsync(
        string username,
        string password, 
        CancellationToken cancellationToken)
    {
        // FIXME: do better with the HttpClient :)
        var response = await new HttpClient()
            .GetAsync("https://api.nexussoftwarelabs.com/auth")
            .ConfigureAwait(false);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException(
                $"Invalid credentials for user {username}");
        }

        var authToken = await response
            .Content
            .ReadAsStringAsync(cancellationToken)
            .ConfigureAwait(false);
        return authToken;
    }
}
