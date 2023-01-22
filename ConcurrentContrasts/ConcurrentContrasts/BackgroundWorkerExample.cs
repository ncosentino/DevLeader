using System.ComponentModel;

internal sealed class BackgroundWorkerExample : IExample
{
    public string Name =>
        "Background Worker";

    public string Description =>
        "Runs a BackgroundWorker that will periodically print to the console. " +
        "This method will exit once you press enter.";

    public void ExecuteExample()
    {
        void DoWork(string label)
        {
            while (true)
            {
                Task.Delay(1000).Wait();
                Console.WriteLine($"Waiting in '{label}'...");
            }
        };

        var backgroundWorker = new BackgroundWorker();
        // NOTE: RunWorkerCompleted may not have a chance to run before the application exits
        backgroundWorker.RunWorkerCompleted += (s, e) => Console.WriteLine("Background worker finished.");
        backgroundWorker.DoWork += (s, e) => DoWork("background worker");
        backgroundWorker.RunWorkerAsync();

        Console.WriteLine("Press enter to exit!");
        Console.ReadLine();
    }
}