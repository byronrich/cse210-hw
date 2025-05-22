using System;

public class Fraction
{
    private int numerator;
    private int denominator;

    // Constructor that initializes to 1/1
    public Fraction()
    {
        numerator = 1;
        denominator = 1;
    }

    // Constructor that initializes with a numerator and denominator of 1
    public Fraction(int numerator)
    {
        this.numerator = numerator;
        this.denominator = 1;
    }

    // Constructor that initializes with both numerator and denominator
    public Fraction(int numerator, int denominator)
    {
        this.numerator = numerator;
        this.denominator = denominator;
    }

    // Getters and setters
    public int GetNumerator()
    {
        return numerator;
    }

    public void SetNumerator(int numerator)
    {
        this.numerator = numerator;
    }

    public int GetDenominator()
    {
        return denominator;
    }

    public void SetDenominator(int denominator)
    {
        this.denominator = denominator;
    }

    // Method to get the fraction as a string
    public string GetFractionString()
    {
        return $"{numerator}/{denominator}";
    }

    // Method to get the decimal value
    public double GetDecimalValue()
    {
        return (double)numerator / denominator;
    }
}

// Example usage in Program.cs
class Program
{
    static void Main()
    {
        Fraction f1 = new Fraction();
        Fraction f2 = new Fraction(5);
        Fraction f3 = new Fraction(3, 4);
        Fraction f4 = new Fraction(1, 3);

        Console.WriteLine(f1.GetFractionString() + " = " + f1.GetDecimalValue());
        Console.WriteLine(f2.GetFractionString() + " = " + f2.GetDecimalValue());
        Console.WriteLine(f3.GetFractionString() + " = " + f3.GetDecimalValue());
        Console.WriteLine(f4.GetFractionString() + " = " + f4.GetDecimalValue());
    }
}
