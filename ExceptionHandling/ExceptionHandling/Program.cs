AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
{
    Console.WriteLine(args.ExceptionObject);
    Console.WriteLine($"Is Terminating: {args.IsTerminating}");
};

//throw new InvalidOperationException("Oh no!");


TaskScheduler.UnobservedTaskException += (sender, args) =>
{
    Console.WriteLine(args.Exception);
    Console.WriteLine($"Is Observed: {args.Observed}");
};

Task.Run(() =>
{
    Thread.Sleep(3000);
    throw new InvalidOperationException("Oh no!");
});
while (true)
{
    await Task.Delay(5000);

    GC.Collect();
}