namespace AiAssistantApi.Models;

public sealed record ChatRequest(string Prompt, string? SystemPrompt = null);
