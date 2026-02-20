using Microsoft.Extensions.AI;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace AiCodingAgent.Tools;

public sealed class CodeTools
{
    private readonly string _workingDirectory;

    public CodeTools(string workingDirectory)
    {
        _workingDirectory = Path.GetFullPath(workingDirectory);
    }

    [Description("Read the contents of a source code file")]
    public string ReadFile(
        [Description("Path to the file (relative to the working directory or absolute)")] string path)
    {
        try
        {
            var fullPath = ResolvePath(path);
            if (!File.Exists(fullPath))
                return $"[Error] File not found: {fullPath}";

            var size = new FileInfo(fullPath).Length;
            if (size > 100_000)
                return $"[Error] File too large ({size:N0} bytes). Use a more specific path.";

            return File.ReadAllText(fullPath);
        }
        catch (Exception ex)
        {
            return $"[Error] Could not read file: {ex.Message}";
        }
    }

    [Description("Write or overwrite a file with the given content, creating any necessary directories")]
    public string WriteFile(
        [Description("Path to the file to write (relative to working directory or absolute)")] string path,
        [Description("The complete file content to write")] string content)
    {
        try
        {
            var fullPath = ResolvePath(path);
            var dir = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(fullPath, content);
            return $"[Success] Written {content.Length:N0} characters to {fullPath}";
        }
        catch (Exception ex)
        {
            return $"[Error] Could not write file: {ex.Message}";
        }
    }

    [Description("List files in a directory, with optional extension filter")]
    public string ListFiles(
        [Description("Directory path (relative to working directory)")] string directory,
        [Description("File pattern filter, e.g. '*.cs', '*.json', '*.*'")] string pattern = "*.*")
    {
        try
        {
            var fullPath = ResolvePath(directory);
            if (!Directory.Exists(fullPath))
                return $"[Error] Directory not found: {fullPath}";

            var sb = new StringBuilder();
            var dirs = Directory.GetDirectories(fullPath)
                .Where(d => !Path.GetFileName(d).StartsWith('.'))
                .Select(d => $"[DIR]  {Path.GetRelativePath(_workingDirectory, d)}/");
            var files = Directory.GetFiles(fullPath, pattern, SearchOption.TopDirectoryOnly)
                .Select(f => $"[FILE] {Path.GetRelativePath(_workingDirectory, f)}");

            foreach (var item in dirs.Concat(files).Take(100))
                sb.AppendLine(item);

            return sb.Length == 0 ? "[Empty directory]" : sb.ToString();
        }
        catch (Exception ex)
        {
            return $"[Error] Could not list directory: {ex.Message}";
        }
    }

    [Description("Search for text across source files in a directory")]
    public string SearchInFiles(
        [Description("Directory to search (relative to working directory)")] string directory,
        [Description("Text to search for")] string searchText,
        [Description("File extension filter, e.g. '*.cs'")] string pattern = "*.cs")
    {
        try
        {
            var fullPath = ResolvePath(directory);
            if (!Directory.Exists(fullPath))
                return $"[Error] Directory not found: {fullPath}";

            var sb = new StringBuilder();
            var matchCount = 0;

            foreach (var file in Directory.GetFiles(fullPath, pattern, SearchOption.AllDirectories))
            {
                var lines = File.ReadAllLines(file);
                for (var i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    {
                        var rel = Path.GetRelativePath(_workingDirectory, file);
                        sb.AppendLine($"{rel}:{i + 1}: {lines[i].Trim()}");
                        if (++matchCount >= 50) break;
                    }
                }
                if (matchCount >= 50) break;
            }

            return matchCount == 0
                ? $"[No matches for '{searchText}' in {pattern} files]"
                : sb.ToString();
        }
        catch (Exception ex)
        {
            return $"[Error] Search failed: {ex.Message}";
        }
    }

    [Description("Run dotnet build on a project or solution file to check for compilation errors")]
    public string RunDotnetBuild(
        [Description("Path to the .csproj or .sln file (relative to working directory)")] string projectPath)
    {
        try
        {
            var fullPath = ResolvePath(projectPath);
            if (!File.Exists(fullPath))
                return $"[Error] Project file not found: {fullPath}";

            var psi = new ProcessStartInfo("dotnet", $"build \"{fullPath}\" --nologo -v quiet")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                WorkingDirectory = _workingDirectory
            };

            using var process = Process.Start(psi)!;
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            process.WaitForExit(30_000);

            var combined = (output + "\n" + error).Trim();
            return $"[Build exit code: {process.ExitCode}]\n{combined}";
        }
        catch (Exception ex)
        {
            return $"[Error] Build failed: {ex.Message}";
        }
    }

    public ICollection<AIFunction> CreateAll() =>
    [
        AIFunctionFactory.Create(ReadFile, name: "read_file"),
        AIFunctionFactory.Create(WriteFile, name: "write_file"),
        AIFunctionFactory.Create(ListFiles, name: "list_files"),
        AIFunctionFactory.Create(SearchInFiles, name: "search_in_files"),
        AIFunctionFactory.Create(RunDotnetBuild, name: "run_dotnet_build"),
    ];

    private string ResolvePath(string path) =>
        Path.IsPathRooted(path)
            ? path
            : Path.GetFullPath(Path.Combine(_workingDirectory, path));
}
