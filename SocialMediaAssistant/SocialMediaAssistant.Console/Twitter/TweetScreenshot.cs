using System.Drawing;

public record TweetScreenshot(
    string TweetId,
    Image Image) : IDisposable
{
    public void Dispose()
    {
        Image?.Dispose();
    }
}
