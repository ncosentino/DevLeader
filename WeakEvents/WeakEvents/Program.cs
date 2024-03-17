using System.Runtime.CompilerServices;

var eventSource = new EventSource();
var listener = new EventListener();
eventSource.CustomEvent.AddListener(listener.OnCustomEvent);

eventSource.TriggerEvent();
eventSource.CustomEvent.RemoveListener(listener.OnCustomEvent);

Console.ReadLine();

public sealed class WeakEvent<TEventArgs> where TEventArgs : EventArgs
{
    private readonly List<WeakReference<EventHandler<TEventArgs>>> _listeners;
    private readonly ConditionalWeakTable<object, List<object>> _delegateKeepAlive;

    public WeakEvent()
    {
        _listeners = [];
        _delegateKeepAlive = [];
    }

    public void AddListener(EventHandler<TEventArgs>? handler)
    {
        if (handler == null)
        {
            return;
        }

        _listeners.Add(new(handler));

        if (handler.Target != null)
        {
            var delegateList = _delegateKeepAlive.GetOrCreateValue(handler.Target);
            delegateList.Add(handler);
        }
    }

    public void RemoveListener(EventHandler<TEventArgs>? handler)
    {
        if (handler == null)
        {
            return;
        }

        _listeners.RemoveAll(wr =>
            wr.TryGetTarget(out var existingHandler) &&
            existingHandler == handler);

        if (handler.Target != null &&
            _delegateKeepAlive.TryGetValue(handler.Target, out var delegateList))
        {
            delegateList.Remove(handler);
        }
    }

    public void Raise(object sender, TEventArgs e)
    {
        foreach (var weakReference in _listeners.ToList())
        {
            if (weakReference.TryGetTarget(out var handler))
            {
                handler(sender, e);
            }
            else
            {
                _listeners.Remove(weakReference);
            }
        }
    }
}

public class EventSource
{
    public WeakEvent<EventArgs> CustomEvent = new WeakEvent<EventArgs>();

    public void TriggerEvent()
    {
        CustomEvent.Raise(this, EventArgs.Empty);
    }
}

public class EventListener
{
    public void OnCustomEvent(object? sender, EventArgs e)
    {
        Console.WriteLine("Event received.");
    }
}
