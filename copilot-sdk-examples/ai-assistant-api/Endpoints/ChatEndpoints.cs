using AiAssistantApi.Models;
using AiAssistantApi.Services;
using System.Diagnostics;

namespace AiAssistantApi.Endpoints;

public static class ChatEndpoints
{
    public static IEndpointRouteBuilder MapChatEndpoints(this IEndpointRouteBuilder app)
    {
        // POST /chat -- full blocking response
        app.MapPost("/chat", async (
            ChatRequest request,
            CopilotService copilot,
            CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return Results.BadRequest("Prompt cannot be empty.");

            var sw = Stopwatch.StartNew();
            var reply = await copilot.ChatAsync(request.Prompt, request.SystemPrompt, ct);
            sw.Stop();

            return Results.Ok(new ChatResponse(reply, sw.ElapsedMilliseconds));
        })
        .WithName("Chat")
        .WithSummary("Send a message and receive a complete response");

        // GET /chat/stream -- Server-Sent Events streaming response
        app.MapGet("/chat/stream", async (
            string prompt,
            string? systemPrompt,
            CopilotService copilot,
            HttpContext ctx,
            CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                ctx.Response.StatusCode = 400;
                await ctx.Response.WriteAsync("prompt query parameter is required", ct);
                return;
            }

            ctx.Response.ContentType = "text/event-stream";
            ctx.Response.Headers.CacheControl = "no-cache";
            ctx.Response.Headers.Connection = "keep-alive";

            await foreach (var chunk in copilot.StreamAsync(prompt, systemPrompt, ct))
            {
                // Escape newlines so each SSE data line stays on one line
                var escaped = chunk.Replace("\r", "").Replace("\n", "\\n");
                await ctx.Response.WriteAsync($"data: {escaped}\n\n", ct);
                await ctx.Response.Body.FlushAsync(ct);
            }

            await ctx.Response.WriteAsync("data: [DONE]\n\n", ct);
        })
        .WithName("ChatStream")
        .WithSummary("Stream a response using Server-Sent Events");

        // GET /health -- liveness check
        app.MapGet("/health", () =>
            Results.Ok(new { Status = "Healthy", Timestamp = DateTimeOffset.UtcNow }))
        .WithName("Health");

        return app;
    }
}
