using ScrutorVsAutofac.SDK;

namespace AutofacPlugin1;

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