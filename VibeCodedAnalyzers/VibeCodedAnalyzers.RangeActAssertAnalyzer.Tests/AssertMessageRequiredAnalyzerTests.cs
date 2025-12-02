using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Xunit;

namespace VibeCodedAnalyzers.RangeActAssertAnalyzer.Tests;

public class AssertMessageRequiredAnalyzerTests
{
    private static async Task VerifyAsync(string source, params DiagnosticResult[] expected)
    {
        var test = new CSharpAnalyzerTest<VibeCodedAnalyzers.RangeActAssert.AssertMessageRequiredAnalyzer, Microsoft.CodeAnalysis.Testing.Verifiers.XUnitVerifier>
        {
            TestCode = source,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            CompilerDiagnostics = CompilerDiagnostics.None
        };

        test.ExpectedDiagnostics.AddRange(expected);
        await test.RunAsync();
    }

    [Fact]
    public async Task ReportsError_WhenAssertTrueMissingMessage()
    {
        var code = @"using Xunit; class C { void M(bool b) { Assert.True(b); } }";
        var expected = new DiagnosticResult(VibeCodedAnalyzers.RangeActAssert.AssertMessageRequiredAnalyzer.DiagnosticId, DiagnosticSeverity.Error)
            .WithSpan(1, 41, 1, 55)
            .WithArguments("True");
        await VerifyAsync(code, expected);
    }

    [Fact]
    public async Task ReportsError_WhenAssertFalseMissingMessage()
    {
        var code = @"using Xunit; class C { void M(bool b) { Assert.False(b); } }";
        var expected = new DiagnosticResult(VibeCodedAnalyzers.RangeActAssert.AssertMessageRequiredAnalyzer.DiagnosticId, DiagnosticSeverity.Error)
            .WithSpan(1, 41, 1, 56)
            .WithArguments("False");
        await VerifyAsync(code, expected);
    }

    [Fact]
    public async Task ReportsError_WhenSecondArgNotString()
    {
        var code = @"using Xunit; class C { void M(bool b) { Assert.True(b, 123); } }";
        var expected = new DiagnosticResult(VibeCodedAnalyzers.RangeActAssert.AssertMessageRequiredAnalyzer.DiagnosticId, DiagnosticSeverity.Error)
            .WithSpan(1, 41, 1, 59)
            .WithArguments("True");
        await VerifyAsync(code, expected);
    }

    [Fact]
    public async Task NoDiagnostic_WhenMessageProvided()
    {
        var code = @"using Xunit; class C { void M(bool b) { Assert.True(b, ""helpful message""); } }";
        await VerifyAsync(code);
    }

    [Fact]
    public async Task NoDiagnostic_WhenFalseHasMessage()
    {
        var code = @"using Xunit; class C { void M(bool b) { Assert.False(b, ""desc""); } }";
        await VerifyAsync(code);
    }
}
