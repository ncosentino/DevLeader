public sealed class IterationDepthFirst
{
    public void Run(string root)
    {
        if (!Directory.Exists(root))
        {
            Console.WriteLine("Path does not exist.");
            return;
        }

        Stack<Entry> folders = new();
        folders.Push(new Entry(root, Path.GetFileName(root), 0));

        while (folders.Count > 0)
        {
            Entry currentFolder = folders.Pop();
            string indentation = new(' ', currentFolder.Level);
            Console.WriteLine($"{indentation}{currentFolder.Name}");

            indentation = new(' ', currentFolder.Level + 1);
            foreach (var file in Directory.GetFiles(currentFolder.Path))
            {
                Console.WriteLine($"{indentation}{Path.GetFileName(file)}");
            }

            foreach (var dir in Directory.GetDirectories(currentFolder.Path))
            {
                Entry newEntry = new(
                    dir, 
                    Path.GetFileName(dir),
                    currentFolder.Level + 1);
                folders.Push(newEntry);
            }
        }
    }
}
