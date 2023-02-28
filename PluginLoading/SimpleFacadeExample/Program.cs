// The code in this file is intended to accompany the video here:
// - No video, sorry!
// You can also follow along in the corresponding blog post here:    
// - https://www.devleader.ca/2023/03/03/facade-pattern-a-beginners-how-to-for-simplified-code/

// let's imagine a situation where we want to submit
// data to a data source
var data = new Data(
    "Dev Leader Website",
    "https://www.devleader.ca");

Console.WriteLine("Starting example 1...");

var originalEmailBasedDataPublisher = new OriginalEmailBasedDataPublisher();
await originalEmailBasedDataPublisher.PublishAsync(
    "https://smtpserver",
    "SomeUsername",
    "SecretPassword123",
    data);

Console.WriteLine("Example 1 complete.");

// the above example works, but let's see how the
// code changes as we want to add in another
// publisher!

Console.WriteLine("Starting example 2...");

// NOTE: we already have our publisher for email
// from previous example
var originalSmsBasedDataPublisher = new OriginalSmsBasedDataPublisher();

await originalEmailBasedDataPublisher.PublishAsync(
    "https://smtpserver",
    "SomeUsername",
    "SecretPassword123",
    data);
await originalSmsBasedDataPublisher.PublishAsync(
    new SmsConfiguration("Some Config Details"),
    data);

Console.WriteLine("Example 2 complete.");

// if we continue this pattern, assuming over
// time our code base is growing larger and
// larger... two things likely continue:
// - We keep adding more publishers that have
//   their own criteria for being able to publish
// - We keep adding more spots to the code base
//   that need to be maintained every time we
//   need to change, add, or remove a publisher
// so what can we do?

Console.WriteLine("Starting example 3...");

var emailPublisher = new EmailBasedDataPublisher(
    "https://smtpserver",
    "SomeUsername",
    "SecretPassword123");
var smsPublisher = new SmsBasedDataPublisher(
    new SmsConfiguration("Some Configuration"));
var publisher = new PublisherFacade(new IDataPublisher[]
{
    emailPublisher,
    smsPublisher
});

await publisher.PublishAsync(data);

Console.WriteLine("Example 3 complete.");

sealed record Data(
    string Name,
    string Value);

sealed class OriginalEmailBasedDataPublisher
{
    public Task PublishAsync(
        string smtpServer,
        string smtpUsername,
        string smtpPassword,
        Data data)
    {
        // TODO: go actually send some email... this is just to demo
        Console.WriteLine($"Sending email for '{data}'...");
        return Task.CompletedTask;
    }
}

sealed record SmsConfiguration(
    string SomePropertiesYouNeedForSmsGoHere);

sealed class OriginalSmsBasedDataPublisher
{
    public Task PublishAsync(
        SmsConfiguration configuration,
        Data data)
    {
        // TODO: go actually send some email... this is just to demo
        Console.WriteLine($"Sending SMS for '{data}'...");
        return Task.CompletedTask;
    }
}

interface IDataPublisher
{
    Task PublishAsync(Data data);
}

sealed class PublisherFacade : IDataPublisher
{
    private readonly IReadOnlyCollection<IDataPublisher> _publishers;

    public PublisherFacade(IEnumerable<IDataPublisher> publishers)
    {
        _publishers = publishers.ToArray();
    }

    public async Task PublishAsync(Data data)
    {
        // NOTE: this could be implemented in many different ways...
        var publishTasks = _publishers.Select(x => x.PublishAsync(data));
        await Task.WhenAll(publishTasks);
    }
}

sealed class EmailBasedDataPublisher : IDataPublisher
{
    public EmailBasedDataPublisher(
        string smtpServer,
        string smtpUsername,
        string smtpPassword)
    {
        // NOTE: we pass in implementation-specific
        // configuration via constructor now
    }

    public Task PublishAsync(Data data)
    {
        // TODO: go actually send some email... this is just to demo
        Console.WriteLine($"Sending email for '{data}'...");
        return Task.CompletedTask;
    }
}

sealed class SmsBasedDataPublisher : IDataPublisher
{
    public SmsBasedDataPublisher(SmsConfiguration configuration)
    {
        // NOTE: we pass in implementation-specific
        // configuration via constructor now
    }

    public Task PublishAsync(Data data)
    {
        // TODO: go actually send some email... this is just to demo
        Console.WriteLine($"Sending SMS for '{data}'...");
        return Task.CompletedTask;
    }
}