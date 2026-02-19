using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Xunit;

namespace VibeCodedAnalyzers.Analyzers.Tests;

public class AssertMessageAnalyzerTests
{
    private static async Task<ImmutableArray<Diagnostic>> GetDiagnosticsAsync(string code)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var refs = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Runtime.AssemblyTargetedPatchBandAttribute).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(FactAttribute).Assembly.Location),
        };
        var compilation = CSharpCompilation.Create("Test",
            new[] { syntaxTree },
            refs,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var analyzer = new AssertMessageAnalyzer();
        var analyzers = ImmutableArray.Create<DiagnosticAnalyzer>(analyzer);
        var compilationWithAnalyzers = compilation.WithAnalyzers(analyzers);
        return await compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync();
    }

    [Fact]
    public async Task Reports_Error_When_True_Has_No_Message()
    {
        var test = @"using Xunit; class T { void M() { Assert.True(1 == 1); } }";
        var diags = await GetDiagnosticsAsync(test);
        Assert.Contains(diags, d => d.Id == AssertMessageAnalyzer.DiagnosticIdTrue);
    }

    [Fact]
    public async Task Reports_Error_When_False_Has_No_Message()
    {
        var test = @"using Xunit; class T { void M() { Assert.False(1 == 2); } }";
        var diags = await GetDiagnosticsAsync(test);
        Assert.Contains(diags, d => d.Id == AssertMessageAnalyzer.DiagnosticIdFalse);
    }

    [Fact]
    public async Task No_Error_When_True_Has_Message()
    {
        var test = @"using Xunit; class T { void M() { Assert.True(1 == 1, ""because math""); } }";
        var diags = await GetDiagnosticsAsync(test);
        Assert.DoesNotContain(diags, d => d.Id == AssertMessageAnalyzer.DiagnosticIdTrue);
    }

    [Fact]
    public async Task No_Error_When_False_Has_Message()
    {
        var test = @"using Xunit; class T { void M() { Assert.False(1 == 2, ""because math""); } }";
        var diags = await GetDiagnosticsAsync(test);
        Assert.DoesNotContain(diags, d => d.Id == AssertMessageAnalyzer.DiagnosticIdFalse);
    }

    [Fact]
    public async Task Reports_Multiple_Errors_For_Multiple_Violations()
    {
        var test = @"
using Xunit;
class T 
{ 
    void M() 
    { 
        Assert.True(1 == 1);
        Assert.False(2 == 3);
        Assert.True(5 > 3);
    } 
}";
        var diags = await GetDiagnosticsAsync(test);
        Assert.Equal(3, diags.Length);
        Assert.Equal(2, diags.Count(d => d.Id == AssertMessageAnalyzer.DiagnosticIdTrue));
        Assert.Single(diags.Where(d => d.Id == AssertMessageAnalyzer.DiagnosticIdFalse));
    }

    [Fact]
    public async Task No_Error_For_Mixed_Valid_And_Invalid_Asserts()
    {
        var test = @"
using Xunit;
class T 
{ 
    void M() 
    { 
        Assert.True(1 == 1, ""valid"");
        Assert.False(2 == 3, ""also valid"");
    } 
}";
        var diags = await GetDiagnosticsAsync(test);
        Assert.Empty(diags);
    }

    [Fact]
    public async Task Reports_Error_With_Correct_Message_For_True()
    {
        var test = @"using Xunit; class T { void M() { Assert.True(1 == 1); } }";
        var diags = await GetDiagnosticsAsync(test);
        var diagnostic = Assert.Single(diags.Where(d => d.Id == AssertMessageAnalyzer.DiagnosticIdTrue));
        Assert.Contains("descriptive message", diagnostic.GetMessage());
        Assert.Contains("second parameter", diagnostic.GetMessage());
        Assert.Contains("Assert.True", diagnostic.GetMessage());
    }

    [Fact]
    public async Task Reports_Error_With_Correct_Message_For_False()
    {
        var test = @"using Xunit; class T { void M() { Assert.False(1 == 2); } }";
        var diags = await GetDiagnosticsAsync(test);
        var diagnostic = Assert.Single(diags.Where(d => d.Id == AssertMessageAnalyzer.DiagnosticIdFalse));
        Assert.Contains("descriptive message", diagnostic.GetMessage());
        Assert.Contains("second parameter", diagnostic.GetMessage());
        Assert.Contains("Assert.False", diagnostic.GetMessage());
    }

    [Fact]
    public async Task Reports_Error_Severity_As_Error()
    {
        var test = @"using Xunit; class T { void M() { Assert.True(1 == 1); } }";
        var diags = await GetDiagnosticsAsync(test);
        var diagnostic = Assert.Single(diags);
        Assert.Equal(DiagnosticSeverity.Error, diagnostic.Severity);
    }

    [Fact]
    public async Task No_Error_For_Non_Xunit_Assert_Methods()
    {
        var test = @"
namespace CustomXunit { public class Assert { public static void True(bool b) { } } }
class T { void M() { CustomXunit.Assert.True(true); } }";
        var diags = await GetDiagnosticsAsync(test);
        // Should not report error for custom Assert class in different namespace
        Assert.Empty(diags);
    }
}
