using AiAssistantApi.Services;
using AiAssistantApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Register CopilotService as a singleton so the same CopilotClient instance
// is shared across all requests, with IHostedService managing its lifecycle.
builder.Services.AddSingleton<CopilotService>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<CopilotService>());

var app = builder.Build();

app.MapChatEndpoints();

app.Run();
