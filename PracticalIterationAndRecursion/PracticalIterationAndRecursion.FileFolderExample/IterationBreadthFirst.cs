public sealed class IterationBreadthFirst
{
    public void Run(string root)
    {
        if (!Directory.Exists(root))
        {
            Console.WriteLine("Path does not exist.");
            return;
        }

        Queue<Entry> folders = new();
        folders.Enqueue(new Entry(root, Path.GetFileName(root), 0));

        while (folders.Count > 0)
        {
            Entry currentFolder = folders.Dequeue();
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
                folders.Enqueue(newEntry);
            }
        }
    }
}
