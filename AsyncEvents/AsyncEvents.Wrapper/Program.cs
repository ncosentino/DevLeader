Console.WriteLine("Starting example...");

var raisingObject = new RaisingObject();

#region Base Case
//raisingObject.Event += async (s, e) =>
//{
//    Console.WriteLine("Starting the event handler...");
//    await TaskThatThrowsAsync();
//    Console.WriteLine("Event handler completed.");
//};
#endregion

#region Solution 1
//raisingObject.Event += async (s, e) =>
//{
//    Console.WriteLine("Starting the event handler...");
//    try
//    {
//        await TaskThatThrowsAsync();
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"[Inside Event Handler] Our exception handler caught: {ex}");
//    }
//    Console.WriteLine("Event handler completed.");
//};
#endregion

#region Solution 2
raisingObject.Event += EventHandlers.TryAsync<EventArgs>(
    async (s, e) =>
    {
        Console.WriteLine("Starting the event handler...");
        await TaskThatThrowsAsync();
        Console.WriteLine("Event handler completed.");
    },
    ex => Console.WriteLine($"[TryAsync Error Callback] Our exception handler caught: {ex}"));
#endregion

try
{
    Console.WriteLine("Raising our event...");
    raisingObject.Raise(EventArgs.Empty);
}
catch (Exception ex)
{
    Console.WriteLine($"Our exception handler caught: {ex}");
}

Console.WriteLine("Example complete.");

async Task TaskThatThrowsAsync()
{
    Console.WriteLine("Starting task that throws async...");
    throw new InvalidOperationException("This is our exception");
};

class RaisingObject
{
    // you could in theory have your own event args here
    public event EventHandler<EventArgs> Event;


    public void Raise(EventArgs e)
    {
        Event?.Invoke(this, e);
    }
}

#region Wrapper Solution
static class EventHandlers
{
    public static EventHandler<TArgs> TryAsync<TArgs>(
        Func<object, TArgs, Task> callback,
        Action<Exception> errorHandler)
        where TArgs : EventArgs
            => TryAsync<TArgs>(
            callback,
            ex =>
            {
                errorHandler.Invoke(ex);
                return Task.CompletedTask;
            });

    public static EventHandler<TArgs> TryAsync<TArgs>(
        Func<object, TArgs, Task> callback,
        Func<Exception, Task> errorHandler)
        where TArgs : EventArgs
    {
        return new EventHandler<TArgs>(async (object s, TArgs e) =>
        {
            try
            {
                await callback.Invoke(s, e);
            }
            catch (Exception ex)
            {
                await errorHandler.Invoke(ex);
            }
        });
    }
}
#endregion