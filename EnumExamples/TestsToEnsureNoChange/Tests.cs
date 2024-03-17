using Xunit;

namespace TestsToEnsureNoChange;

public enum DevLeaderEnum
{
    Dev,
    Value3,
    Leader,
}

public class Tests
{
    [Fact]
    public void Enum_Names_Should_Not_Change()
    {
        var expected = new[]
        {
            "Dev",
            "Leader",
            "Value3",
        };

        var actual = Enum.GetNames<DevLeaderEnum>();

        Assert.Equal(expected, actual);
    }

    
    [Fact]
    public void INACCURATE_Enum_Values_Should_Not_Change()
    {
        var expected = new[]
        {
            0,
            1,
            2
        };

        var actual = Enum
            .GetValues<DevLeaderEnum>()
            .Select(x => (int)x);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Enum_Values_Should_Not_Change()
    {
        var expected = new Dictionary<string, int>()
        {
            ["Dev"] = 0,
            ["Leader"] = 1,
            ["Value3"] = 2
        };

        var actual = Enum
            .GetValues<DevLeaderEnum>()
            .ToDictionary(
                x => x.ToString(),
                x => (int)x);

        Assert.Equal(expected, actual);
    }
}
