using Microsoft.Extensions.AI;
using System.ComponentModel;

namespace AiAssistantApi.Tools;

public static class CalculatorTool
{
    [Description("Add two numbers together")]
    public static double Add(
        [Description("First number")] double a,
        [Description("Second number")] double b) => a + b;

    [Description("Multiply two numbers together")]
    public static double Multiply(
        [Description("First number")] double a,
        [Description("Second number")] double b) => a * b;

    [Description("Calculate a percentage of a value")]
    public static double Percentage(
        [Description("The base value")] double value,
        [Description("The percentage to calculate (0-100)")] double percent) =>
        value * (percent / 100.0);

    public static ICollection<AIFunction> CreateAll() =>
    [
        AIFunctionFactory.Create(Add, name: "add"),
        AIFunctionFactory.Create(Multiply, name: "multiply"),
        AIFunctionFactory.Create(Percentage, name: "percentage"),
    ];
}
