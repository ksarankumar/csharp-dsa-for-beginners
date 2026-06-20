using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 3 — String Interpolation &amp; Formatting
///
/// Goal: learn the modern, readable way to build strings in C#:
///   - $"..." interpolation
///   - Number, currency, and percentage formatting
///   - Alignment / padding (handy for printing neat tables of results)
///   - Multi-line "raw" strings
/// </summary>
public sealed class Module03_StringInterpolation : ILesson
{
    public int Order => 3;
    public string Title => "String Interpolation & Formatting";

    public void Run()
    {
        string language = "C#";
        int year = 2026;

        // --- BASIC INTERPOLATION -------------------------------------
        // The $ before the quotes lets you drop variables straight into { }.
        Console.WriteLine($"I'm learning {language} in {year}.");

        // You can even run expressions inside the braces.
        int a = 7, b = 6;
        Console.WriteLine($"{a} x {b} = {a * b}");

        Console.WriteLine();

        // --- NUMBER FORMATTING ---------------------------------------
        // After a colon you add a "format specifier".
        double price = 1234.5;
        Console.WriteLine($"Currency : {price:C}");   // C = currency (uses your locale's symbol)
        Console.WriteLine($"2 decimals: {price:F2}"); // F2 = fixed-point, 2 digits
        Console.WriteLine($"Thousands : {price:N0}"); // N0 = number with separators, 0 decimals

        double ratio = 0.8734;
        Console.WriteLine($"Percentage: {ratio:P1}"); // P1 = percent with 1 decimal

        Console.WriteLine();

        // --- ALIGNMENT / PADDING -------------------------------------
        // {value,width} pads to a column width.
        //   positive width = right-aligned, negative width = left-aligned.
        // This is perfect for printing tidy tables — common when showing algorithm results.
        Console.WriteLine($"|{"Name",-10}|{"Score",6}|");
        Console.WriteLine($"|{"Alice",-10}|{95,6}|");
        Console.WriteLine($"|{"Bob",-10}|{120,6}|");

        Console.WriteLine();

        // --- RAW / MULTI-LINE STRINGS --------------------------------
        // Triple quotes (""") create a raw string literal: great for blocks of text,
        // JSON, or ASCII art without fighting escape characters.
        string box = """
            +-----------------+
            |  Keep going!    |
            +-----------------+
            """;
        Console.WriteLine(box);

        Console.WriteLine();
        Console.WriteLine("Tip: $\"...\" is easier to read than string.Concat or the old {0} style.");
    }
}
