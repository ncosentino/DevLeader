using Xunit;

namespace VibeCodedAnalyzers.ConsoleApp.Tests;

public class FunWithNumbersTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(43)]
    public void IsNumberEven_ReturnsTrue(int input)
    {
        var sut = new FunWithNumbers();
        var result = sut.IsNumberEven(input);
        Assert.True(result, $"Expected {input} to be even.");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(101)]
    public void IsNumberEven_ReturnsFalse(int input)
    {
        var sut = new FunWithNumbers();
        var result = sut.IsNumberEven(input);
        Assert.False(result, $"Expected {input} to be odd (not even).");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(101)]
    public void IsNumberOdd_ReturnsTrue(int input)
    {
        var sut = new FunWithNumbers();
        var result = sut.IsNumberOdd(input);
        Assert.True(result, $"Expected {input} to be odd.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(42)]
    public void IsNumberOdd_ReturnsFalse(int input)
    {
        var sut = new FunWithNumbers();
        var result = sut.IsNumberOdd(input);
        Assert.False(result, $"Expected {input} to be even (not odd).");
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(17)]
    [InlineData(19)]
    public void IsNumberPrime_ReturnsTrue(int input)
    {
        var sut = new FunWithNumbers();
        var result = sut.IsNumberPrime(input);
        Assert.True(result, $"Expected {input} to be prime.");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(4)]
    [InlineData(16)]
    [InlineData(21)]
    public void IsNumberPrime_ReturnsFalse(int input)
    {
        var sut = new FunWithNumbers();
        var result = sut.IsNumberPrime(input);
        Assert.False(result, $"Expected {input} to be non-prime.");
    }

    [Theory]
    [InlineData(6)]    // 1 + 2 + 3 = 6
    [InlineData(28)]   // 1 + 2 + 4 + 7 + 14 = 28
    [InlineData(496)]  // known perfect number
    public void IsNumberPerfect_ReturnsTrue(int input)
    {
        var sut = new FunWithNumbers();
        var result = sut.IsNumberPerfect(input);
        Assert.True(result, $"Expected {input} to be a perfect number.");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(12)]
    [InlineData(27)]
    public void IsNumberPerfect_ReturnsFalse(int input)
    {
        var sut = new FunWithNumbers();
        var result = sut.IsNumberPerfect(input);
        Assert.False(result, $"Expected {input} to not be a perfect number.");
    }
}
