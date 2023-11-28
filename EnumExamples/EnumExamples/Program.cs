//var dayOfTheWeek = DayOfTheWeek.Monday;

/* Example 1
//Console.WriteLine("Example 1");
//Console.WriteLine(dayOfTheWeek);
//Console.WriteLine(dayOfTheWeek.Equals("Monday"));
//Console.WriteLine(dayOfTheWeek.Equals(0));
*/

/* Example 2
Console.WriteLine();
Console.WriteLine("Example 2");
Console.WriteLine((DayOfTheWeek)0);
Console.WriteLine((DayOfTheWeek)1);
Console.WriteLine((DayOfTheWeek)3);
Console.WriteLine((DayOfTheWeek)5);
*/

/* Example 3
// This won't work!
//Console.WriteLine();
//Console.WriteLine("Example 3");
//Console.WriteLine((DayOfTheWeek)"Monday");
*/

/* Parse Examples
var didParse1 = Enum.TryParse<DayOfTheWeek>(
    "wednesday", 
    out var tryParse1);
Console.WriteLine($"{tryParse1}, {didParse1}");

var didParse2 = Enum.TryParse<DayOfTheWeek>(
    "tuesday",
    ignoreCase: true,
    out var tryParse2);
Console.WriteLine($"{tryParse2}, {didParse2}");

var didParse3 = Enum.TryParse<DayOfTheWeek>(
    "Friday",
    ignoreCase: false,
    out var tryParse3);
Console.WriteLine($"{tryParse3}, {didParse3}");
*/

//public enum DayOfTheWeek
//{
//    Monday,
//    Tuesday,
//    Wednesday,
//    Thursday,
//    Friday,
//    Saturday,
//    Sunday
//}

//var flagsAWithC = MyFlags.A | MyFlags.C;
//Console.WriteLine($"Flags A With C: {flagsAWithC}");

//var flagsAOverlappingWithC = MyFlags.A & MyFlags.C;
//Console.WriteLine($"Flags A Overlapping With C: {flagsAOverlappingWithC}");

//var someFlags = MyFlags.A | MyFlags.B | MyFlags.C; //0b0000_0111
//Console.WriteLine($"Some Flags: {someFlags}");
//Console.WriteLine($"Is Flag A On?: {(someFlags & MyFlags.A) == MyFlags.A}");
//Console.WriteLine($"Is Flag D On?: {(someFlags & MyFlags.D) == MyFlags.D}");
//Console.WriteLine($"Is Flag C On?: {(someFlags & MyFlags.C) == MyFlags.C}");

//[Flags]
//public enum MyFlags
//{
//    A = 0b0001,
//    B = 0b0010,
//    C = 0b0100,
//    D = 0b1000,
//}

public sealed class ProductProcessor
{
    public void HandleProductsIf(Product product)
    {
        if (product == Product.DevLeaderSub1)
        {
            // do stuff
        }
        else if (product == Product.DevLeaderCourse1)
        {
            // do stuff
        }
        else
        {
            // exceptional case or something...
        }
    }

    public void HandleProductsSwitch(Product product)
    {
        switch (product)
        {
            case Product.DevLeaderCourse1:
                // do stuff
                break;
            default:
                // exceptional case or something...
                break;
        }    
    }

    public void HandleProductsMapping(Product product)
    {
        // note: this would likely go at
        // instance level as a field
        Dictionary<Product, Action> mapping = new()
        {
            { Product.DevLeaderCourse1, () => { /* do stuff */ } },
            { Product.DevLeaderSub1, () => { /* do stuff */ } },
        };

        if (mapping.TryGetValue(product, out var action))
        {
            action();
        }
        else
        {
            // exceptional case or something...
        }
    }
}

public enum Product
{
    // courses
    DevLeaderCourse1,
    DevLeaderCourse2,
    DevLeaderCourse3,
    // subscriptions
    DevLeaderSub1,
    DevLeaderSub2,
    DevLeaderSub3,
    // e-books
    DevLeaderEBook1,
    DevLeaderEBook2,
    DevLeaderEBook3,
}