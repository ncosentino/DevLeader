using System.ComponentModel;

internal sealed class SimultaneousExample : IExample
{
    public string Name =>
        "Simultaneous Thread, BackgroundWorker, and Task";

    public string Description =>
        "Runs a Thread, BackgoundWorker, and Task at the same time. Each will " +
        "periodically write to the console. This method includes cancellation " +
        "tokens into each of the three approaches so their concurrent work " +
        "will stop when the cancellation token is cancelled. The thread is not " +
        "marked as background, but thanks to the cancellation token it should " +
        "not block the program from exiting.";

    public void ExecuteExample()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        void DoWork(string label)
        {
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                Task.Delay(1000).Wait();
                Console.WriteLine($"Waiting in '{label}'...");
            }
        };

        var thread = new Thread(new ThreadStart(() => DoWork("thread")));
        thread.Start();

        var backgroundWorker = new BackgroundWorker();
        // NOTE: RunWorkerCompleted may not have a chance to run before the application exits
        backgroundWorker.RunWorkerCompleted += (s, e) => Console.WriteLine("Background worker finished.");
        backgroundWorker.DoWork += (s, e) => DoWork("background worker");
        backgroundWorker.RunWorkerAsync();

        var task = Task.Run(() => DoWork("task"));

        Console.WriteLine("Press enter to exit!");
        Console.ReadLine();
        cancellationTokenSource.Cancel();
    }
}
