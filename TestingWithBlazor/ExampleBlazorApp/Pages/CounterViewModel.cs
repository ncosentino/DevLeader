namespace ExampleBlazorApp.Pages;

public sealed class CounterViewModel : ICounterViewModel
{
    public int CurrentCount { get; private set; }

    public void IncrementCount() =>
        CurrentCount++;
}
