using Python.Runtime;

/// <summary>
/// This code is for an article originally published here:
/// https://www.devleader.ca/2023/01/20/pythonnet-a-simple-union-of-net-core-and-python-youll-love/
/// </summary>
internal sealed class Program
{
    private static void Main(string[] args)
    {
        // NOTE: set this based on your python install. this will resolve from
        // your PATH environment variable as well.
        Runtime.PythonDLL = "python310.dll";

        PythonEngine.Initialize();
        using (Py.GIL())
        {
            using var scope = Py.CreateScope();

            scope.Exec("number_list = [1, 2, 3, 4, 5]");
            var pythonListObj = scope.Eval("number_list");
            var csharpListObj = pythonListObj.As<int[]>();

            Console.WriteLine("The numbers from python are:");
            foreach (var value in csharpListObj)
            {
                Console.WriteLine(value);
            }

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }

    //private static void Main(string[] args)
    //{
    //    // NOTE: set this based on your python install. this will resolve from
    //    // your PATH environment variable as well.
    //    Runtime.PythonDLL = "python310.dll";

    //    PythonEngine.Initialize();
    //    using (Py.GIL())
    //    {
    //        // NOTE: this doesn't validate input
    //        Console.WriteLine("Enter first integer:");
    //        var firstInt = int.Parse(Console.ReadLine());

    //        Console.WriteLine("Enter second integer:");
    //        var secondInt = int.Parse(Console.ReadLine());

    //        using dynamic scope = Py.CreateScope();
    //        scope.Exec("def add(a, b): return a + b");
    //        int sum = scope.add(firstInt, secondInt);
    //        Console.WriteLine($"Sum: {sum}");
    //    }
    //}

    //private static void Main(string[] args)
    //{
    //     NOTE: set this based on your python install. this will resolve from
    //     your PATH environment variable as well.
    //    Runtime.PythonDLL = "python310.dll";

    //    PythonEngine.Initialize();
    //    using (Py.GIL())
    //    {
    //        using var scope = Py.CreateScope();
    //        scope.Exec("print('Hello World from Python!')");
    //    }
    //}
}