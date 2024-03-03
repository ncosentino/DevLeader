//
// You can find the corresponding article for these code snippers
// on my website at:
// https://www.devleader.ca/2024/03/04/implicit-operators-in-c-how-to-simplify-type-conversions/
//
Console.WriteLine("Implicit Operators");

Console.WriteLine("Implicit conversion from length and temperature to double");
double notAnObviousResult = 
    new LengthInM { Value = 5.5 } * 
    new TemperatureInC { Value = 1.23 };
Console.WriteLine(notAnObviousResult); // Output: 6.765
Console.WriteLine();

Console.WriteLine("Implicit conversion from int and double to complex number");
ComplexNumber complexNumber = 2.5; // Implicit conversion from double
complexNumber += 3; // Implicit conversion from int
Console.WriteLine($"Real: {complexNumber.Real}"); // Output: 5.5
Console.WriteLine($"Imaginary: {complexNumber.Imaginary}"); // Output: 0
Console.WriteLine();

Console.WriteLine("Implicit conversion from money to double");
Money moneyInWallet = new Money(100.50); // $100.50
double cash = moneyInWallet; // Implicit conversion to double

// Adding more money
moneyInWallet += 99.50; // Implicit conversion from double to Money, then addition

Console.WriteLine($"Cash: {cash}"); // Output: 100.5
Console.WriteLine($"Money in Wallet: {moneyInWallet.Amount}"); // Output: 200

public struct LengthInM
{
    public double Value { get; set; }

    // Implicit conversion from Length to double
    public static implicit operator double(LengthInM length)
    {
        return length.Value;
    }
}

public struct TemperatureInC
{
    public double Value { get; set; }

    // Implicit conversion from Temperature to double
    public static implicit operator double(TemperatureInC temperature)
    {
        return temperature.Value;
    }
}
public struct ComplexNumber
{
    public double Real { get; set; }
    public double Imaginary { get; set; }

    public ComplexNumber(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    // Implicitly converts int to ComplexNumber
    public static implicit operator ComplexNumber(int number)
    {
        return new ComplexNumber(number, 0);
    }

    // Implicitly converts double to ComplexNumber
    public static implicit operator ComplexNumber(double number)
    {
        return new ComplexNumber(number, 0);
    }

    // Adds two ComplexNumber instances
    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }
}

public struct Money
{
    private readonly double _amount;

    public Money(double amount)
    {
        _amount = amount;
    }

    public double Amount => _amount;

    // Implicitly converts Money to double
    public static implicit operator double(Money money)
    {
        return money._amount;
    }

    // Implicitly converts double to Money
    public static implicit operator Money(double amount)
    {
        return new Money(amount);
    }
}