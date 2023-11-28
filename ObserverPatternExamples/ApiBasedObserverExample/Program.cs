var observable = new Observable();
var observer = new Observer();
observable.AddObserver(observer);

observable.FireEvent("Hello, World!");

public interface IObserver
{
    void HandleEvent(string message);
}

public sealed class Observable
{
    private List<IObserver> _observers;

    public Observable()
    {
        _observers = new List<IObserver>();
    }

    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void FireEvent(string message)
    {
        foreach (var observer in _observers)
        {
            observer.HandleEvent(message);
        }
    }
}

public sealed class Observer : IObserver
{
    public void HandleEvent(string message)
    {
        Console.WriteLine($"Event fired: {message}");
    }
}
