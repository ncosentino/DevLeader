using Xunit;

namespace XUnitTheoryData;

public class AdderTests
{
    [MemberData(nameof(AdderTestData))]
    //[ClassData(typeof(CalculatorTestData))]
    [Theory]
    public void Add_ValidInputs_ExpectedValue(
        AdderTestScenario scenario)
    {
        var adder = new Adder(); 
        var result = adder.Add(
            scenario.Number1, 
            scenario.Number2);
        Assert.Equal(scenario.ExpectedResult, result);
    }

    //public static IEnumerable<object[]> AdderTestData()
    //{
    //    yield return new object[] { 0, 0, 0 };
    //    yield return new object[] { 1, 2, 3 };
    //    yield return new object[] { 2, 3, 5 };
    //    yield return new object[] { 3, 4, 7 };
    //    yield return new object[] { -100, -200, -300 };
    //}

    
    //public static TheoryData<string, int, int> AdderTestData
    //{
    //    get
    //    {
    //        TheoryData<string, int, int> data = new()
    //        {
    //            { "0", 0, 0 },
    //            //{ 1, 2, 3 },
    //            //{ 2, 3, 5 },
    //            //{ 3, 4, 7 },
    //            //{ -100, -200, -300 }
    //        };
    //        return data;
    //    }
    //}

    public static TheoryData<AdderTestScenario> AdderTestData
    {
        get
        {
            TheoryData<AdderTestScenario> data = new()
            {
                new AdderTestScenario(0, 0, 0),
                new AdderTestScenario(1, 2, 3),
                new AdderTestScenario(2, 3, 5),
                new AdderTestScenario(3, 4, 7),
                new AdderTestScenario(- 100, -200, -300),
            };
            return data;
        }
    }

    public sealed record AdderTestScenario(
        int Number1,
        int Number2, 
        int ExpectedResult);
}

public sealed class Adder
{
    public int Add(int number1, int number2) => number1 + number2;
}

public class CalculatorTestData : TheoryData<string, int, int>
{
    public CalculatorTestData()
    {
        //Add(0, 0, 0);
        //Add(1, 2, 3);
        //Add(2, 3, 5);
        //Add(3, 4, 7);
        //Add(-100, -200, -300);
        Add("", 0, 0);
    }
}