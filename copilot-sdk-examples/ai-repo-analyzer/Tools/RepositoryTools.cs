using Microsoft.Extensions.AI;
using System.ComponentModel;
using System.Text;

namespace AiRepoAnalyzer.Tools;

public sealed class RepositoryTools
{
    private readonly string _repoRoot;

    public RepositoryTools(string repoRoot)
    {
        _repoRoot = Path.GetFullPath(repoRoot);
    }

    [Description("List the directory structure of the repository, showing folders and files up to a given depth")]
    public string ListStructure(
        [Description("Subdirectory to list, relative to repo root (use '.' for root)")] string directory = ".",
        [Description("File pattern filter, e.g. '*.cs', '*.csproj', or '*' for all")] string pattern = "*",
        [Description("Maximum depth to recurse (1 = top level only, default 2)")] int maxDepth = 2)
    {
        var fullPath = ResolvePath(directory);
        if (!Directory.Exists(fullPath))
            return $"[Error] Directory not found: {directory}";

        var sb = new StringBuilder();
        AppendTree(fullPath, fullPath, pattern, 0, maxDepth, sb);
        return sb.Length == 0 ? "[Empty directory]" : sb.ToString();
    }

    [Description("Read the content of a file in the repository")]
    public string ReadFile(
        [Description("Path to the file, relative to the repository root")] string path)
    {
        var fullPath = ResolvePath(path);
        if (!File.Exists(fullPath))
            return $"[Error] File not found: {path}";

        var size = new FileInfo(fullPath).Length;
        if (size > 60_000)
            return $"[Error] File too large ({size:N0} bytes). Consider reading a subsection.";

        return File.ReadAllText(fullPath);
    }

    [Description("Find all files matching a name pattern anywhere in the repository")]
    public string FindFiles(
        [Description("File name pattern, e.g. '*.csproj', 'README*', 'Program.cs'")] string pattern)
    {
        var files = Directory.GetFiles(_repoRoot, pattern, SearchOption.AllDirectories)
            .Select(f => Path.GetRelativePath(_repoRoot, f))
            .Where(f => !f.StartsWith(".git", StringComparison.OrdinalIgnoreCase))
            .OrderBy(f => f)
            .Take(50)
            .ToList();

        return files.Count == 0
            ? $"[No files matching '{pattern}']"
            : string.Join("\n", files);
    }

    [Description("Count how many times a text pattern appears across source files to understand usage frequency")]
    public string CountUsage(
        [Description("Text pattern to count occurrences of")] string pattern,
        [Description("File extension filter, e.g. '*.cs'")] string fileExtension = "*.cs")
    {
        var files = Directory.GetFiles(_repoRoot, fileExtension, SearchOption.AllDirectories)
            .Where(f => !f.Contains(".git", StringComparison.OrdinalIgnoreCase));

        var total = 0;
        var fileMatches = new List<(string File, int Count)>();

        foreach (var file in files)
        {
            var content = File.ReadAllText(file);
            var count = CountOccurrences(content, pattern);
            if (count > 0)
            {
                fileMatches.Add((Path.GetRelativePath(_repoRoot, file), count));
                total += count;
            }
        }

        if (total == 0)
            return $"[Pattern '{pattern}' not found in {fileExtension} files]";

        var sb = new StringBuilder();
        sb.AppendLine($"Total occurrences of '{pattern}': {total}");
        foreach (var (file, count) in fileMatches.OrderByDescending(x => x.Count).Take(20))
            sb.AppendLine($"  {count,4}x  {file}");

        return sb.ToString();
    }

    public ICollection<AIFunction> CreateAll() =>
    [
        AIFunctionFactory.Create(ListStructure, name: "list_structure"),
        AIFunctionFactory.Create(ReadFile, name: "read_file"),
        AIFunctionFactory.Create(FindFiles, name: "find_files"),
        AIFunctionFactory.Create(CountUsage, name: "count_usage"),
    ];

    private void AppendTree(string basePath, string current, string pattern, int depth, int maxDepth, StringBuilder sb)
    {
        var indent = new string(' ', depth * 2);

        if (depth < maxDepth)
        {
            foreach (var dir in Directory.GetDirectories(current)
                .Where(d => !Path.GetFileName(d).StartsWith('.'))
                .OrderBy(d => d))
            {
                sb.AppendLine($"{indent}{Path.GetFileName(dir)}/");
                AppendTree(basePath, dir, pattern, depth + 1, maxDepth, sb);
            }
        }

        var matchPattern = pattern == "*" ? "*.*" : pattern;
        foreach (var file in Directory.GetFiles(current, matchPattern).OrderBy(f => f).Take(50))
            sb.AppendLine($"{indent}{Path.GetFileName(file)}");
    }

    private static int CountOccurrences(string text, string pattern)
    {
        var count = 0;
        var index = 0;
        while ((index = text.IndexOf(pattern, index, StringComparison.OrdinalIgnoreCase)) >= 0)
        {
            count++;
            index += pattern.Length;
        }
        return count;
    }

    private string ResolvePath(string path) =>
        Path.IsPathRooted(path)
            ? path
            : Path.GetFullPath(Path.Combine(_repoRoot, path));
}
