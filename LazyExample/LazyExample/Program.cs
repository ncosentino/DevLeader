// all of our initialization and startup code...
Console.WriteLine($"{DateTime.Now}: Starting...");
Lazy<Task<int>> magicNumber = new Lazy<Task<int>>(DoExpensiveOperationOnceAsync);
Console.WriteLine($"{DateTime.Now}: Started.");

// all of our working code for the application...
Console.WriteLine($"{DateTime.Now}: Starting work...");

var theMagicNumber = await magicNumber.Value;
DoWork(theMagicNumber);

// no penalty to get the value again from the lazy object
Console.WriteLine($"The magic number is {theMagicNumber}");
Console.WriteLine($"The magic number is {theMagicNumber}");
Console.WriteLine($"The magic number is {theMagicNumber}");
Console.WriteLine($"The magic number is {theMagicNumber}");

Console.WriteLine($"{DateTime.Now}: Finished work.");


async Task<int> DoExpensiveOperationOnceAsync()
{
    // simulate a long running operation
    await Task.Delay(2000);
    return 123;
}

void DoWork(int magicNumber)
{
    // simulate a long running operation
    Thread.Sleep(3000);
    Console.WriteLine($"The magic number is {magicNumber}");
}