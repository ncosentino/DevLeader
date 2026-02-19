namespace AiCodeReviewBot.Models;

public sealed class ReviewResult
{
    public string FileName { get; init; } = string.Empty;
    public string Review { get; init; } = string.Empty;
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
}
