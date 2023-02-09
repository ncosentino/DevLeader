#region First Example
Console.WriteLine("Starting first example...");

var ourOldObject = new OldRaisingObject();
ourOldObject.Event += async (s, e) =>
{
    Console.WriteLine("Starting the event handler...");
    await TaskThatThrowsAsync();
    Console.WriteLine("Event handler completed.");
};

try
{
    Console.WriteLine("Raising our event...");
    ourOldObject.Raise(EventArgs.Empty);
}
catch (Exception ex)
{
    Console.WriteLine($"Our exception handler caught: {ex}");
}

Console.WriteLine("First example complete.");
#endregion

#region Second Example
//Console.WriteLine("Starting second example...");
//var ourFancyObject = new FancyRaisingObject();
//ourFancyObject.ExplicitAsyncEvent += async (s, e) =>
//{
//    Console.WriteLine("Starting the event handler...");
//    await TaskThatThrowsAsync();
//    Console.WriteLine("Event handler completed.");
//};

//try
//{
//    Console.WriteLine("Raising our async event...");
//    await ourFancyObject.RaiseAsync(EventArgs.Empty);
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"Our exception handler caught: {ex}");
//}

//Console.WriteLine("Second example complete.");
#endregion

async Task TaskThatThrowsAsync()
{
    Console.WriteLine("Starting task that throws async...");
    throw new InvalidOperationException("This is our exception");
};

#region New Way - Setup
delegate Task AsyncEventHandler<TArgs>(object sender, TArgs e)
    where TArgs : EventArgs;

class FancyRaisingObject
{
    // you could in theory have your own event args here
    public event AsyncEventHandler<EventArgs> ExplicitAsyncEvent;


    public async Task RaiseAsync(EventArgs e)
    {
        await ExplicitAsyncEvent?.Invoke(this, e);
    }
}
#endregion

#region Old Way - Setup
class OldRaisingObject
{
    // you could in theory have your own event args here
    public event EventHandler<EventArgs> Event;


    public void Raise(EventArgs e)
    {
        Event?.Invoke(this, e);
    }
}
#endregion