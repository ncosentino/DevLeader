using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Xunit;

namespace VibeCodedAnalyzers.Analyzers.Tests;

public class ArrangeActAssertCommentAnalyzerTests
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

        var analyzer = new VibeCodedAnalyzers.Analyzers.ArrangeActAssertCommentAnalyzer();
        var analyzers = ImmutableArray.Create<DiagnosticAnalyzer>(analyzer);
        var compilationWithAnalyzers = compilation.WithAnalyzers(analyzers);
        return await compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync();
    }

    [Fact]
    public async Task Reports_Error_For_AAA_Comments_In_Fact_Test()
    {
        var test = @"using Xunit; public class T { [Fact] public void M() { // Arrange\n int x=1; // Act\n x++; // Assert\n Assert.True(x==2); } }";
        var diags = await GetDiagnosticsAsync(test);
        Assert.True(diags.Any(d => d.Id == VibeCodedAnalyzers.Analyzers.ArrangeActAssertCommentAnalyzer.DiagnosticId));
        Assert.Equal(3, diags.Count(d => d.Id == VibeCodedAnalyzers.Analyzers.ArrangeActAssertCommentAnalyzer.DiagnosticId));
    }

    [Fact]
    public async Task Reports_Error_For_AAA_Comments_In_Theory_Test()
    {
        var test = @"using Xunit; public class T { [Theory] public void M() { // Arrange\n int x=1; // Act\n x++; // Assert\n Assert.True(x==2); } }";
        var diags = await GetDiagnosticsAsync(test);
        Assert.Equal(3, diags.Count(d => d.Id == VibeCodedAnalyzers.Analyzers.ArrangeActAssertCommentAnalyzer.DiagnosticId));
    }

    [Fact]
    public async Task Reports_Error_For_Block_AAA_Comments()
    {
        var test = @"using Xunit; public class T { [Fact] public void M() { /* Arrange */ int x=1; /* Act */ x++; /* Assert */ Assert.True(x==2); } }";
        var diags = await GetDiagnosticsAsync(test);
        Assert.Equal(3, diags.Count(d => d.Id == VibeCodedAnalyzers.Analyzers.ArrangeActAssertCommentAnalyzer.DiagnosticId));
    }

    [Fact]
    public async Task No_Error_When_No_AAA_Comments()
    {
        var test = @"using Xunit; public class T { [Fact] public void M() { int x=1; x++; Assert.True(x==2); } }";
        var diags = await GetDiagnosticsAsync(test);
        Assert.DoesNotContain(diags, d => d.Id == VibeCodedAnalyzers.Analyzers.ArrangeActAssertCommentAnalyzer.DiagnosticId);
    }

    [Fact]
    public async Task Ignores_Non_Test_Methods()
    {
        var test = @"class T { public void M() { // Arrange\n int x=1; // Act\n x++; // Assert\n } }";
        var diags = await GetDiagnosticsAsync(test);
        Assert.Empty(diags);
    }
}
