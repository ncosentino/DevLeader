using Microsoft.AspNetCore.Diagnostics;

using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(opts =>
{
    opts.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
#if DEBUG
    opts.SerializerOptions.WriteIndented = true;
#endif
});
builder.Services.AddSingleton<StandardResponseFilter>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseExceptionHandler(x => x.Run(ErrorHandler));

var weather = app
    .MapGroup("weather")
    .AddEndpointFilter<StandardResponseFilter>();

weather.MapGet("/forecast", (HttpRequest request) =>
{
    if (!IsRequestValid(request))
    {
        throw new ValidationException(
            "Validation failed... Missing ID for X.");
    }

    return new
    {
        Value = 123,
        SomethingElse = (string?)null,
    };
});
weather.MapGet("/forecast2", (HttpRequest request) =>
{
    if (!IsRequestValid(request))
    {
        throw new ValidationException(
            "Validation failed... Missing ID for X.");
    }

    return new
    {
        Value = 123,
        SomethingElse = (string?)null,
    };
});

app.Run();

bool IsRequestValid(HttpRequest request)
{
    return true;
}

async Task ErrorHandler(HttpContext context)
{
    var exceptionFeature = context
        .Features
        .Get<IExceptionHandlerPathFeature>();
    var error = exceptionFeature.Error;

    if (error is ValidationException)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsJsonAsync(new ApiResponse(null, new
        {
            Message = error.Message,
#if DEBUG
            ExceptionType = error.GetType().Name,
            StackTrace = error.StackTrace
#endif
        }));
        return;
    }

    context.Response.StatusCode = 400;
    await context.Response.WriteAsJsonAsync(new ApiResponse(null, new
    {
        Message = "An error occurred while processing your request.",
    }));
}

internal record ApiResponse(
    object? Data,
    object? Error)
{
    public bool Success => Error == null;
}

public sealed class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }
}

public class StandardResponseFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var result = await next(context);

        if (result is ApiResponse)
        {
            return result;
        }

        return new ApiResponse(result, null);
    }
}