internal sealed class NonBackgroundThreadExample : IExample
{
    public string Name =>
        "Non-Background Thread";

    public string Description =>
        "Runs a Thread that is not marked as background that will " +
        "periodically print to the console. This method will exit once you " +
        "press enter; However, the non-background nature of the thread will " +
        "keep the executable running.";

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
        thread.Start();

        Console.WriteLine("Press enter to exit!");
        Console.ReadLine();
    }
}
