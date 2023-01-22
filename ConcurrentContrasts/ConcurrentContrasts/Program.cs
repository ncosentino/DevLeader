using System.Globalization;

internal sealed class Program
{
    private static readonly IReadOnlyDictionary<int, IExample> _examples =
        new Dictionary<int, IExample>()
        {
            [1] = new NonBackgroundThreadExample(),
            [2] = new BackgroundThreadExample(),
            [3] = new BackgroundWorkerExample(),
            [4] = new SimultaneousExample(),
        };

    private static void Main(string[] args)
    {
        Console.WriteLine("Enter the number for one of the following examples to run:");
        foreach (var entry in _examples)
        {
            Console.WriteLine("----");
            var restoreColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Choice: {entry.Key}");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Name: {entry.Value.Name}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Description: {entry.Value.Description}");
            Console.ForegroundColor = restoreColor;
        }

        Console.WriteLine("----");

        IExample example;
        while (true)
        {
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Would you like to exit? Y/N");
                input = Console.ReadLine();
                if ("y".Equals(input, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                Console.WriteLine("Please make another selection.");
                continue;
            }

            if (!int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out var exampleId) ||
                !_examples.TryGetValue(exampleId, out example))
            {
                Console.WriteLine("Invalid input. Please make another selection.");
                continue;
            }

            break;
        }

        Console.WriteLine($"Starting example '{example.Name}'...");
        Console.WriteLine("-- Before entering example method");
        example.ExecuteExample();
        Console.WriteLine("-- After leaving example method");
    }
}
