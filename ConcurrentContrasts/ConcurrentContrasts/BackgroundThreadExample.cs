internal sealed class BackgroundThreadExample : IExample
{
    public string Name =>
        "Background Thread";

    public string Description =>
        "Runs a Thread that is marked as background that will " +
        "periodically print to the console. This method will exit once you " +
        "press enter. Because the thread is marked as background, it will not " +
        "prevent the application from exiting.";

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

        var thread = new Thread(new ThreadStart(() => DoWork("thread")));
        thread.IsBackground = true;
        thread.Start();

        Console.WriteLine("Press enter to exit!");
        Console.ReadLine();
    }
}
