using ScrutorVsAutofac.SDK;

namespace ScrutorPlugin1;

public sealed class MyService : IMyService
{
    public Task RunAsync(
        string input,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(input);
        return Task.CompletedTask;
    }
}