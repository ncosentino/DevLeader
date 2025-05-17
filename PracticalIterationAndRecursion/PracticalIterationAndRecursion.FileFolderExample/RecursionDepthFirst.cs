

//public sealed class RecursionDepthFirst
//{
//    public void Run(string root)
//    {
//        if (!Directory.Exists(root))
//        {
//            Console.WriteLine("Path does not exist.");
//            return;
//        }

//        DepthFirstRecursive(root);
//    }

//    static void DepthFirstRecursive(string currentFolder)
//    {
//        Console.WriteLine($"Folder: {currentFolder}");

//        foreach (var file in Directory.GetFiles(currentFolder))
//        {
//            Console.WriteLine($"  File: {Path.GetFileName(file)}");
//        }

//        foreach (var dir in Directory.GetDirectories(currentFolder))
//        {
//            DepthFirstRecursive(dir);
//        }
//    }

//    //static void DepthFirstRecursive(string currentFolder)
//    //{
//    //    Console.WriteLine($"Folder: {currentFolder}");

//    //    var files = Directory.GetFiles(currentFolder);
//    //    ProcessFilesRecursive(files, 0);

//    //    var subdirectories = Directory.GetDirectories(currentFolder);
//    //    ProcessDirectoriesRecursive(subdirectories, 0);
//    //}

//    //static void ProcessFilesRecursive(string[] files, int index)
//    //{
//    //    // base case
//    //    if (index >= files.Length)
//    //    {
//    //        return;
//    //    }

//    //    Console.WriteLine($"  File: {Path.GetFileName(files[index])}");

//    //    ProcessFilesRecursive(files, index + 1);
//    //}

//    //// Helper method to process directories recursively
//    //static void ProcessDirectoriesRecursive(string[] directories, int index)
//    //{
//    //    // base case
//    //    if (index >= directories.Length)
//    //    {
//    //        return;
//    //    }

//    //    DepthFirstRecursive(directories[index]);

//    //    ProcessDirectoriesRecursive(directories, index + 1);
//    //}
//}