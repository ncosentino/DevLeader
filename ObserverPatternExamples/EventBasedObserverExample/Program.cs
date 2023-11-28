var observable = new EventBasedObservable();
var observer = new EventBasedObserver(observable);

observable.FireEvent("Hello, World!");

public sealed class MessageEventArgs : EventArgs
{
    public string Message { get; }

    public MessageEventArgs(string message)
    {
        Message = message;
    }
}

public sealed class EventBasedObservable
{
    public event EventHandler<MessageEventArgs>? Event;
    
    public void FireEvent(string message)
    {
        Event?.Invoke(this, new(message));
    }
}

public sealed class EventBasedObserver
{
    public EventBasedObserver(EventBasedObservable observable)
    {
        observable.Event += OnEvent;
    }
    
    private void OnEvent(object? sender, MessageEventArgs e)
    {
        Console.WriteLine($"Event fired: {e.Message}");
    }
}