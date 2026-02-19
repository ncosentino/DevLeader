using Xunit;

namespace VibeCodedAnalyzers.ConsoleApp.Tests;

public class FunWithNumbersTests
{
    private readonly bool _someField = false;

    [Theory]
    [InlineData(0, true)]
    [InlineData(2, true)]
    [InlineData(42, true)]
    [InlineData(1, false)]
    [InlineData(3, false)]
    [InlineData(99, false)]
    public void IsNumberEven_ReturnsExpected(int input, bool expected)
    {
        var sut = new FunWithNumbers();
        var result = sut.IsNumberEven(input);
        Assert.Equal(expected, result);


        Assert.True(1 == 2, $"Expected to get true for the expression.");
        Assert.False(result, $"Expected to get false for '{nameof(result)}'.");
        Assert.True(DoesSomething(), $"Expected to get true for '{nameof(DoesSomething)}'.");
        Assert.False(_someField, $"Expected to get false for '{nameof(_someField)}'.");
    }

    private bool DoesSomething()
    {
        return false;
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(2, false)]
    [InlineData(42, false)]
    [InlineData(1, true)]
    [InlineData(3, true)]
    [InlineData(99, true)]
    public void IsNumberOdd_ReturnsExpected(int input, bool expected)
    {
        var sut = new FunWithNumbers();
        var result = sut.IsNumberOdd(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-1, false)]
    [InlineData(0, false)]
    [InlineData(1, false)]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(4, false)]
    [InlineData(5, true)]
    [InlineData(9, false)]
    [InlineData(11, true)]
    [InlineData(12, false)]
    public void IsNumberPrime_ReturnsExpected(int input, bool expected)
    {
        var sut = new FunWithNumbers();
        var result = sut.IsNumberPrime(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-1, false)]
    [InlineData(0, false)]
    [InlineData(1, false)]
    [InlineData(6, true)]
    [InlineData(28, true)]
    [InlineData(496, true)]
    [InlineData(12, false)]
    [InlineData(27, false)]
    public void IsNumberPerfect_ReturnsExpected(int input, bool expected)
    {
        var sut = new FunWithNumbers();
        var result = sut.IsNumberPerfect(input);
        Assert.Equal(expected, result);
    }
}
