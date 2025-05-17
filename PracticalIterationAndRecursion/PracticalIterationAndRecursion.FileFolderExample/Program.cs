string path = @"C:\dev\Courses";
RecursionDepthFirst implementation = new();
implementation.Run(path);


public sealed class RecursionDepthFirst
{
    public void Run(string root)
    {
        if (!Directory.Exists(root))
        {
            Console.WriteLine("Path does not exist.");
            return;
        }

        DepthFirstRecursive2(root, depth: 0);
    }

    //static void DepthFirstRecursive(string currentFolder, int depth)
    //{
    //    string indentation = new(' ', depth);
    //    Console.WriteLine($"{indentation}{Path.GetFileName(currentFolder)}");

    //    foreach (var dir in Directory.GetDirectories(currentFolder))
    //    {
    //        DepthFirstRecursive(dir, depth + 1);
    //    }

    //    indentation = new(' ', depth + 1);
    //    foreach (var file in Directory.GetFiles(currentFolder))
    //    {
    //        Console.WriteLine($"{indentation}{Path.GetFileName(file)}");
    //    }
    //}

    static void DepthFirstRecursive2(string currentFolder, int depth)
    {
        string indentation = new(' ', depth);
        Console.WriteLine($"{indentation}{Path.GetFileName(currentFolder)}");

        var subdirectories = Directory.GetDirectories(currentFolder);
        ProcessDirectoriesRecursive(subdirectories, index: 0, depth: depth);

        var files = Directory.GetFiles(currentFolder);
        ProcessFilesRecursive(files, index: 0, depth: depth);
    }

    static void ProcessFilesRecursive(string[] files, int index, int depth)
    {
        // base case
        if (index >= files.Length)
        {
            return;
        }

        string indentation = new(' ', depth + 1);
        Console.WriteLine($"{indentation}{Path.GetFileName(files[index])}");

        ProcessFilesRecursive(files, index: index + 1, depth: depth);
    }

    static void ProcessDirectoriesRecursive(string[] directories, int index, int depth)
    {
        // base case
        if (index >= directories.Length)
        {
            return;
        }

        DepthFirstRecursive2(directories[index], depth: depth + 1);
        ProcessDirectoriesRecursive(directories, index: index + 1, depth: depth);
    }
}