using Moq;

using SomeThirdPartyAssembly;

using Xunit;

namespace UnitTests;

public class NicksSystemTests
{
    private readonly MockRepository _mockRepository;
    private readonly NicksSystem _nicksSystem;
    private readonly Mock<ICoolClient> _coolClient;
    private readonly Mock<ILogger> _logger;
    private readonly NicksConfig _nicksConfig;
    private readonly CancellationToken _cancellationToken;

    public NicksSystemTests()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);
        _cancellationToken = CancellationToken.None;

        _coolClient = _mockRepository.Create<ICoolClient>();
        _logger = _mockRepository.Create<ILogger>();
        _nicksConfig = new NicksConfig(
            Guid.NewGuid().ToString(), // NOTE: you probably don't want to do this for readability
            Guid.NewGuid().ToString());
        _nicksSystem = new NicksSystem(
            _nicksConfig,
            _coolClient.Object,
            _logger.Object);
    }

    [Fact]
    public async Task DoNiceToHaveWorkWithAuth_UnauthorizedException_CodePathCompletesNoRetries()
    {
        // Arrange
        var exception = new UnauthorizedAccessException("Invalid credentials for user DevLeader");
        _coolClient
            .Setup(x => x.LoginAsync(
                _nicksConfig.Username,
                _nicksConfig.Password,
                _cancellationToken))
            .Throws(exception);

        // Act
        await _nicksSystem.DoNiceToHaveWorkWithAuth(_cancellationToken);

        // Assert
        _coolClient.Verify(x => x.LoginAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<CancellationToken>()),
            Times.Exactly(1));
        _logger.Verify(x => x.LogError(
            It.IsAny<Exception>(),
            It.IsAny<string>()),
            Times.Never);
        _mockRepository.VerifyAll();
    }

    [MemberData(nameof(ExceptionsToRetryTestData))]
    [Theory]
    public async Task DoNiceToHaveWorkWithAuth_ClientThrowsException_RetriesThreeTimes(
        Exception expectedException)
    {
        // Arrange
        _coolClient
            .Setup(x => x.LoginAsync(
                _nicksConfig.Username,
                _nicksConfig.Password,
                _cancellationToken))
            .Throws(expectedException);
        _logger
            .Setup(x => x.LogError(
                expectedException,
                "Failed to get auth token."));

        // Act
        await _nicksSystem.DoNiceToHaveWorkWithAuth(_cancellationToken);

        // Assert
        _coolClient.Verify(x => x.LoginAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<CancellationToken>()),
            Times.Exactly(3));
        _logger.Verify(x => x.LogError(
            expectedException,
            "Failed to get auth token."),
            Times.Exactly(3));
        _mockRepository.VerifyAll();
    }

    public static IEnumerable<object[]> ExceptionsToRetryTestData()
    {
        yield return new object[] { new InvalidOperationException("Something unexpected happened!") };
        yield return new object[] { new HttpRequestException("Something unexpected happened!") };
        yield return new object[] { new UriFormatException("Something unexpected happened!") };
    }
}
