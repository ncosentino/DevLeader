EventSource eventSource = new();
eventSource.Event += async /*void*/ (sender, args) =>
{    
    try
    {
        await Awaitable();
        //await NotAwaitable();
        //NotAwaitable();
    }
    catch (Exception ex)
    {
        Console.WriteLine(
            $"We gracefully caught the " +
            $"exception in the handler: {ex}");
        throw;
    }
};

try
{
    eventSource.RaiseEvent();
}
catch (Exception ex)
{
    Console.WriteLine(
        $"We gracefully caught the exception " +
        $"when raising: {ex}");
}

Console.WriteLine("Press enter to exit.");
Console.ReadLine();

async void NotAwaitable()
{
    await Task.Yield();
    throw new InvalidOperationException(
        "Who will catch this for us?");
}

async Task Awaitable()
{
    await Task.Yield();
    throw new InvalidOperationException(
        "We can catch this one!");
}

class EventSource
{
    public event EventHandler<EventArgs> Event;

    public void RaiseEvent()
    {
        Event?.Invoke(this, EventArgs.Empty);
    }
}