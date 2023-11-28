using System.Reactive.Subjects;

using Observable observable = new();
Observer observer = new();
observable.Subscribe(observer);

observable.Publish(new("Hello, World!"));
observable.Publish(new("Goodbye, World!"));

public sealed record Message(
    string body);

public class Observer : IObserver<Message>
{
    public void OnCompleted()
    {
        Console.WriteLine($"OnCompleted fired");
    }

    public void OnError(Exception error)
    {
        // TODO: Implement OnError method
    }

    public void OnNext(Message value)
    {
        Console.WriteLine($"OnNext fired: {value.body}");
    }
}

public class Observable : 
    IObservable<Message>,
    IDisposable
{
    private readonly Subject<Message> _subject;

    public Observable()
    {
        _subject = new Subject<Message>();
    }

    public void Dispose()
    {
        _subject.OnCompleted();
        _subject.Dispose();
    }

    public IDisposable Subscribe(IObserver<Message> observer)
    {
        return _subject.Subscribe(observer);
    }

    public void Publish(Message value)
    {
        _subject.OnNext(value);
    }
}
