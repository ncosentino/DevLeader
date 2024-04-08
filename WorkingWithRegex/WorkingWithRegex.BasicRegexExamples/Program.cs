//
// You can learn more about these examples here:
// https://www.devleader.ca/2024/04/02/regular-expressions-in-csharp-3-examples-you-need-to-know/
//
using System.Text.RegularExpressions;

//StartsWithExample();
//EndsWithExample();
IsMatchExample();

void StartsWithExample()
{
    Console.WriteLine("StartsWith Example...");

    List<string> inputs = new()
    {
        "Hello, World!",
        "Something something Hello!",
        "  Hello",
        "Hello from Dev Leader!",
    };

    string pattern = "^Hello";
    Regex regex = new(pattern);

    foreach (var input in inputs)
    {
        Match match = regex.Match(input);
        Console.WriteLine(
            $"'{input}' {(match.Success ? "did" : "did not")} " +
            "match the string starting with the pattern.");
    }
}

void EndsWithExample()
{
    Console.WriteLine("EndsWith Example...");

    List<string> inputs = new()
    {
        "Coding", // match
        "I love programming!", // no match
        "Coding is fun!", // no match
        "I love programming", // match
    };

    string pattern = "ing$";
    Regex regex = new Regex(pattern);

    foreach (var input in inputs)
    {
        Match match = regex.Match(input);
        Console.WriteLine(
            $"'{input}' {(match.Success ? "did" : "did not")} " +
            "match the string ending with the pattern.");
    }
}

void IsMatchExample()
{
    Console.WriteLine("IsMatch Example...");
    List<string> inputs = new()
    {
        "Nick", // no match
        "Nick1", // no match
        "1Nick", // no match
        "Nick42", // match
        "42Nick", // match
        "4Nick2", // match
        "42", // match
        "1337", // match
        "6", // no match
    };

    string pattern = "[0-9]+[a-zA-Z]*[0-9]+";
    Regex regex = new Regex(pattern);

    foreach (var input in inputs)
    {
        bool isMatch = regex.IsMatch(input);
        Console.WriteLine(
            $"'{input}' {(isMatch ? "did" : "did not")} " +
            "match the pattern.");
    }
}