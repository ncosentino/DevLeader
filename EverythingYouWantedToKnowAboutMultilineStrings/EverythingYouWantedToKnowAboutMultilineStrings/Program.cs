//
// This code is for a blog post that can be found here:
// https://www.devleader.ca/2023/02/01/everything-you-wanted-to-know-about-multiline-strings/
//
using System;

// this is the most simple example, where we just assign some text between
// double quotes to a string variable. this is our base case to compare against
// when we start looking to multiline things.
var simpleString = "Hello, World!";
Console.WriteLine(simpleString);
Console.WriteLine();

// this example shows the carriage return (\r) and linefeed (\n) characters
// added into the string to make the output span across two lines. these
// line endings can be different on Windows vs unix so consider using
// Environment.NewLine. it's also important to note that we use the backslash
// here which makes it an escape character (and that's why you don't see an
// extra r and n in the console output)
var multilineString = "Hello,\r\nWorld!";
Console.WriteLine(multilineString);
Console.WriteLine();

// like the previous example, except we will use the + operator to concatenate
// the strings. this string is now across multiple lines in our code AND
// multiple lines in the output
var multilineString2 = 
    "Hello,\r\n" +
    "World!";
Console.WriteLine(multilineString2);
Console.WriteLine();

// like the previous example, we will use the + operator to concatenate strings,
// but because we removed all of the \r\n characters this will print on one
// line in the console
var stringAcrossMultipleLines =
    "Hello" +
    "," +
    " " +
    "World" +
    "!";
Console.WriteLine(stringAcrossMultipleLines);
Console.WriteLine();

// this example uses the @ symbol to mark the string as a "verbatim" string.
// by doing this, we can more easily type out longer strings in a visually
// appealing way in the editor. But it has an impact on how it's printed to
// the console as well! You need to consider your indentation levels.
var verbatimString = @"Hello,
    world! I wonder if this 
    string will have funny 
    gaps even though it
    looks like it is 
    aligned to the same
    indentation in the editor...";
Console.WriteLine(verbatimString);
Console.WriteLine();

// this example uses raw string literals, which are denoted by triple quotes. to
// escape triple quotes inside of a verbatim string, you simply would declare
// the verbatim string with 4 quotes instead of 3. This pattern continues for
// more quotes. Compared to a verbatim string, this format does not map the
// indentation of your code to the final string value. another important note
// is this is a C# 11.0 language feature, and we are in a .NET48 app! so we are
// using PolySharp to be able to polyfill and give us this super power at
// compile time!
var rawStringLiteral = 
// Note: try playing with the indentation of all of the lines here
        """
        "Hello,
        world!" I wonder if this 
        string will have funny 
        gaps even though it
        looks like it is 
        aligned to the same
        indentation in the editor...
            Maybe just
            these lines
            right here.
        """;
Console.WriteLine(rawStringLiteral);
Console.WriteLine();