using System.ComponentModel;

namespace AiCliTool.Tools;

/// <summary>
/// File system tools exposed to the AI via AIFunctionFactory.
/// The AI can invoke these to read code files and explore directories.
/// </summary>
public sealed class FileSystemTools
{
    private const int MaxFileSizeBytes = 50_000; // 50KB safety limit

    [Description("Read the contents of a source code file. Use this when the user asks you to review, explain, or analyze a specific file.")]
    public string ReadFile(
        [Description("The absolute or relative path to the file to read.")]
        string path)
    {
        try
        {
            var fullPath = Path.GetFullPath(path);
            if (!File.Exists(fullPath))
                return $"[Error] File not found: {fullPath}";

            var fileInfo = new FileInfo(fullPath);
            if (fileInfo.Length > MaxFileSizeBytes)
                return $"[Error] File is too large ({fileInfo.Length:N0} bytes). Max is {MaxFileSizeBytes:N0} bytes.";

            return File.ReadAllText(fullPath);
        }
        catch (Exception ex)
        {
            return $"[Error] Could not read file: {ex.Message}";
        }
    }

    [Description("List files in a directory matching an optional glob pattern. Useful for exploring a project structure before reading specific files.")]
    public string ListFiles(
        [Description("The directory path to list. Use '.' for the current directory.")]
        string directory,
        [Description("Optional glob pattern to filter files, e.g. '*.cs', '*.json'. Leave empty to list all files.")]
        string pattern = "*")
    {
        try
        {
            var fullPath = Path.GetFullPath(directory);
            if (!Directory.Exists(fullPath))
                return $"[Error] Directory not found: {fullPath}";

            var files = Directory.GetFiles(fullPath, pattern, SearchOption.TopDirectoryOnly);
            var dirs = Directory.GetDirectories(fullPath);

            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"Contents of: {fullPath}");
            sb.AppendLine();

            foreach (var dir in dirs.OrderBy(d => d))
                sb.AppendLine($"[DIR]  {Path.GetFileName(dir)}/");

            foreach (var file in files.OrderBy(f => f))
            {
                var info = new FileInfo(file);
                sb.AppendLine($"[FILE] {Path.GetFileName(file)}  ({info.Length:N0} bytes)");
            }

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return $"[Error] Could not list directory: {ex.Message}";
        }
    }

    [Description("Get the current working directory of the CLI tool.")]
    public string GetCurrentDirectory()
    {
        return Directory.GetCurrentDirectory();
    }
}
