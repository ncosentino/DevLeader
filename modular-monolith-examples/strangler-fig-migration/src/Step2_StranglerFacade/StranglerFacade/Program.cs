var builder = WebApplication.CreateBuilder(args);

// This is the STRANGLER FACADE using YARP.
// It acts as a reverse proxy that routes requests to either:
// - The new Products.Service (for /products/**)
// - The legacy monolith (for everything else)
//
// This allows us to incrementally migrate functionality while maintaining
// a single entry point for clients.

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapReverseProxy();

app.Run();
