namespace AiCodeReviewBot.Configuration;

public sealed class AIProviderConfig
{
    public string Type { get; set; } = "openai";
    public string ModelId { get; set; } = "gpt-4o";
    public string? Endpoint { get; set; }
    public string ApiKey { get; set; } = string.Empty;
}
