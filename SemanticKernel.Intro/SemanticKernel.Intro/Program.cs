using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

using System.ComponentModel;
using System.Text;

using YoutubeExplode;
using YoutubeExplode.Channels;

var modelId = "gpt-4o-mini";
var endpoint = "https://website.openai.azure.com/";
var apiKey = "AAABBCCC";

var builder = Kernel
    .CreateBuilder()
    .AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);
builder.Services.AddLogging(services => services
    .AddConsole()
    .SetMinimumLevel(LogLevel.Information));
Kernel kernel = builder.Build();

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

kernel.Plugins.Add(KernelPluginFactory.CreateFromType<YouTubeCaptionsPlugin>());
kernel.Plugins.Add(KernelPluginFactory.CreateFromType<YouTubeVideosPlugin>());

OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
{
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
};

Console.WriteLine("Write your message to the AI bot!");

var history = new ChatHistory();
string? userInput;
while (true)
{
    userInput = Console.ReadLine();
    if (string.IsNullOrEmpty(userInput))
    {
        break;
    }

    history.AddUserMessage(userInput);

    var result = await chatCompletionService.GetChatMessageContentAsync(
        history,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);
    history.AddMessage(result.Role, result.Content ?? string.Empty);

    Console.WriteLine($"AI: {result.Content}");
}

//public class LightsPlugin
//{
//    // Mock data for the lights
//    private readonly List<LightModel> lights = new()
//    {
//        new LightModel { Id = 1, Name = "Table Lamp", IsOn = false },
//        new LightModel { Id = 2, Name = "Porch light", IsOn = false },
//        new LightModel { Id = 3, Name = "Chandelier", IsOn = true }
//    };

//    [KernelFunction("get_lights")]
//    [Description("Gets a list of lights and their current state")]
//    [return: Description("An array of lights")]
//    public Task<List<LightModel>> GetLightsAsync()
//    {
//        return Task.FromResult(lights);
//    }

//    [KernelFunction("change_state")]
//    [Description("Changes the state of the light")]
//    [return: Description("The updated state of the light; will return null if the light does not exist")]
//    public Task<LightModel?> ChangeStateAsync(
//        [Description("This is the identifier of the light.")]
//        int id,
//        [Description("True if the light is on; false if the light is off.")]
//        bool isOn)
//    {
//        var light = lights.FirstOrDefault(light => light.Id == id);
//        if (light is null)
//        {
//            return Task.FromResult((LightModel?)null);
//        }

//        light.IsOn = isOn;
//        return Task.FromResult(light);
//    }
//}

//public class LightModel
//{
//    [JsonPropertyName("id")]
//    public int Id { get; set; }

//    [JsonPropertyName("name")]
//    public string Name { get; set; }

//    [JsonPropertyName("is_on")]
//    public bool? IsOn { get; set; }
//}

internal class YouTubeCaptionsPlugin
{
    [KernelFunction("get_youtube_video_captions")]
    [Description("Gets the English captions from the entire YouTube video that is specified by the provided videoUrl.")]
    [return: Description("The English captions for the YouTube video without any timestamps.")]
    public async Task<Response<YouTubeCaptions>> GetVideoCaptionsAsync(
        [Description("The url of the video that is available for public viewing on YouTube.")]
        string videoUrl,
        CancellationToken cancellationToken)
    {
        YoutubeClient youtube = new();

        var trackManifest = await youtube.Videos.ClosedCaptions.GetManifestAsync(
            videoUrl,
            cancellationToken);
        if (trackManifest.Tracks.Count == 0)
        {
            return new(new Exception("No captions found for the video."));
        }

        var trackInfo = trackManifest.Tracks.FirstOrDefault(x => string.Equals(x.Language.Code, "en"));
        if (trackInfo is null)
        {
            return new(new Exception("No English captions found for the video."));
        }

        var track = await youtube.Videos.ClosedCaptions.GetAsync(
            trackInfo,
            cancellationToken);

        StringBuilder captionsBuilder = new();
        foreach (var caption in track.Captions)
        {
            captionsBuilder.AppendLine(caption.Text.Trim());
        }

        return new(new YouTubeCaptions(
            videoUrl,
            captionsBuilder.ToString()));
    }
}

internal class YouTubeVideosPlugin
{
    [KernelFunction("get_youtube_videos")]
    [Description("Gets the list of YouTube videos for a channel given the channel's handle.")]
    [return: Description("The collection of YouTube videos that have been uploaded to the YouTube channel.")]
    public async Task<Response<IReadOnlyList<YouTubeVideo>>> GetVideosAsync(
        [Description("The YouTube channel handle like @DevLeader.")]
        string channelHandle,
        CancellationToken cancellationToken)
    {
        channelHandle = channelHandle.TrimStart('@');

        YoutubeClient youtube = new();
        var channel = await youtube.Channels.GetByHandleAsync(
            new ChannelHandle(channelHandle), 
            cancellationToken);
        var videos = await youtube.Channels
            .GetUploadsAsync(channel.Id, cancellationToken)
            .Where(x => x.Duration > TimeSpan.FromSeconds(60))
            .Take(10)
            .ToArrayAsync(cancellationToken);
        return new(videos
            .Select(x => new YouTubeVideo(x.Title, x.Url))
            .ToArray());
    }
}

public sealed record YouTubeVideo(
    string Title,
    string Url);

public sealed record YouTubeCaptions(
    string VideoUrl,
    string Captions);

public sealed record Response<T>
{
    public Response(T value)
    {
        Value = value;
    }

    public Response(Exception error)
    {
        Error = error;
    }

    public T? Value { get; }

    public Exception? Error { get; }

    public bool IsSuccess => Error is null;

    public bool IsFailure => Error is not null;
}