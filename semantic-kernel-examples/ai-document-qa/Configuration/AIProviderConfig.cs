namespace AiDocumentQA.Configuration;

public sealed class AIProviderConfig
{
    /// <summary>Provider type: "azureopenai" or "openai"</summary>
    public string Type { get; set; } = "openai";

    public string ModelId { get; set; } = "";
    public string Endpoint { get; set; } = "";
    public string ApiKey { get; set; } = "";
}
