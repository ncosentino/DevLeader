namespace ExampleBlazorApp.Pages;

public interface ICounterViewModel
{
    int CurrentCount { get; }

    void IncrementCount();
}