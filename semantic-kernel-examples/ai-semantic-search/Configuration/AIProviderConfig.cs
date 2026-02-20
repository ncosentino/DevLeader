namespace AiSemanticSearch.Configuration;

/// <summary>
/// Configuration for an AI provider (OpenAI or Azure OpenAI).
/// Shared for both chat completion and text embedding sections.
/// </summary>
public sealed class AIProviderConfig
{
    /// <summary>"openai" or "azureopenai"</summary>
    public string Type { get; set; } = "openai";

    /// <summary>Model or deployment name (e.g., text-embedding-ada-002)</summary>
    public string ModelId { get; set; } = "";

    /// <summary>API key for the provider</summary>
    public string ApiKey { get; set; } = "";

    /// <summary>Azure OpenAI endpoint URL (required for azureopenai type)</summary>
    public string Endpoint { get; set; } = "";
}
