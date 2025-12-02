using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Xunit;

namespace VibeCodedAnalyzers.RangeActAssertAnalyzer.Tests;

public class RangeActAssertCommentAnalyzerTests
{
    private static async Task VerifyAsync(string source, params DiagnosticResult[] expected)
    {
        var test = new CSharpAnalyzerTest<VibeCodedAnalyzers.RangeActAssert.RangeActAssertCommentAnalyzer, Microsoft.CodeAnalysis.Testing.Verifiers.XUnitVerifier>
        {
            TestCode = source,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            CompilerDiagnostics = CompilerDiagnostics.None
        };

        test.ExpectedDiagnostics.AddRange(expected);
        await test.RunAsync();
    }

    [Fact]
    public async Task ReportsError_ForArrangeComment()
    {
        var code = @"using System; class C { void M() { // Arrange var x = 1; } }";
        var expected = new DiagnosticResult(VibeCodedAnalyzers.RangeActAssert.RangeActAssertCommentAnalyzer.DiagnosticId, DiagnosticSeverity.Error)
            .WithSpan(1, 36, 1, 61)
            .WithArguments("Arrange");
        await VerifyAsync(code, expected);
    }

    [Fact]
    public async Task ReportsError_ForActComment()
    {
        var code = @"using System; class C { void M() { // Act var x = 1; } }";
        var expected = new DiagnosticResult(VibeCodedAnalyzers.RangeActAssert.RangeActAssertCommentAnalyzer.DiagnosticId, DiagnosticSeverity.Error)
            .WithSpan(1, 36, 1, 57)
            .WithArguments("Act");
        await VerifyAsync(code, expected);
    }

    [Fact]
    public async Task ReportsError_ForAssertComment()
    {
        var code = @"using System; class C { void M() { // Assert var x = 1; } }";
        var expected = new DiagnosticResult(VibeCodedAnalyzers.RangeActAssert.RangeActAssertCommentAnalyzer.DiagnosticId, DiagnosticSeverity.Error)
            .WithSpan(1, 36, 1, 60)
            .WithArguments("Assert");
        await VerifyAsync(code, expected);
    }

    [Fact]
    public async Task ReportsError_ForArrangeBlockComment()
    {
        var code = @"using System; class C { void M() { /* Arrange */ var x = 1; } }";
        var expected = new DiagnosticResult(VibeCodedAnalyzers.RangeActAssert.RangeActAssertCommentAnalyzer.DiagnosticId, DiagnosticSeverity.Error)
            .WithSpan(1, 36, 1, 64)
            .WithArguments("Arrange");
        await VerifyAsync(code, expected);
    }

    [Fact]
    public async Task ReportsError_ForActBlockComment()
    {
        var code = @"using System; class C { void M() { /* Act */ var x = 1; } }";
        var expected = new DiagnosticResult(VibeCodedAnalyzers.RangeActAssert.RangeActAssertCommentAnalyzer.DiagnosticId, DiagnosticSeverity.Error)
            .WithSpan(1, 36, 1, 60)
            .WithArguments("Act");
        await VerifyAsync(code, expected);
    }

    [Fact]
    public async Task ReportsError_ForAssertBlockComment()
    {
        var code = @"using System; class C { void M() { /* Assert */ var x = 1; } }";
        var expected = new DiagnosticResult(VibeCodedAnalyzers.RangeActAssert.RangeActAssertCommentAnalyzer.DiagnosticId, DiagnosticSeverity.Error)
            .WithSpan(1, 36, 1, 63)
            .WithArguments("Assert");
        await VerifyAsync(code, expected);
    }

    [Fact]
    public async Task NoDiagnostic_WhenNoBlockedComments()
    {
        var code = @"using System; class C { void M() { // something else var x = 1; } }";
        await VerifyAsync(code);
    }

    [Fact]
    public async Task NoDiagnostic_ForUnrelatedBlockComment()
    {
        var code = @"using System; class C { void M() { /* just a note */ var x = 1; } }";
        await VerifyAsync(code);
    }
}
