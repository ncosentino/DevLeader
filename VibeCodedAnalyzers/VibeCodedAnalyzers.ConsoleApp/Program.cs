FunWithNumbers numbers = new();
Console.WriteLine(numbers.IsNumberEven(42));


public sealed class FunWithNumbers
{
    public bool IsNumberEven(int number)
    {
        return number % 2 == 0;
    }

    public bool IsNumberOdd(int number)
    {
        return !IsNumberEven(number);
    }

    public bool IsNumberPrime(int number)
    {
        if (number <= 1) return false;
        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0) return false;
        }
        return true;
    }

    public bool IsNumberPerfect(int number)
    {
        if (number < 1) return false;
        int sumOfDivisors = 0;
        for (int i = 1; i <= number / 2; i++)
        {
            if (number % i == 0) sumOfDivisors += i;
        }
        return sumOfDivisors == number;
    }
}