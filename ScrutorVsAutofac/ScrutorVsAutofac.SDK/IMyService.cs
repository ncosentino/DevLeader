namespace ScrutorVsAutofac.SDK;

public interface IMyService
{
    Task RunAsync(
        string input,
        CancellationToken cancellationToken);
}