using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 1 — Input &amp; Output
///
/// Goal: learn the handful of methods you'll use in almost every console program:
///   - Console.WriteLine / Console.Write  (output)
///   - Console.ReadLine                   (input as text)
///   - Converting text input into numbers safely with int.TryParse
///
/// In DSA interviews you constantly read input and print results, so getting
/// comfortable here pays off immediately.
/// </summary>
public sealed class Module01_InputOutput : ILesson
{
    public int Order => 1;
    public string Title => "Input / Output Basics";

    public void Run()
    {
        // --- OUTPUT --------------------------------------------------
        // WriteLine prints the text AND moves to a new line.
        Console.WriteLine("Console.WriteLine prints a line and adds a line break.");

        // Write prints WITHOUT a trailing line break — notice the two pieces join together.
        Console.Write("Console.Write keeps the cursor ");
        Console.WriteLine("on the same line.");

        Console.WriteLine(); // an empty line for spacing

        // --- INPUT ---------------------------------------------------
        // ReadLine() always returns the user's input as a STRING (text).
        // It can be null if the input stream ends, so the '?' marks it nullable.
        Console.Write("What's your name? ");
        string? name = Console.ReadLine();

        // If the user just pressed Enter, give them a friendly default.
        if (string.IsNullOrWhiteSpace(name))
        {
            name = "future engineer";
        }

        Console.WriteLine($"Nice to meet you, {name}!");

        // --- TURNING TEXT INTO NUMBERS -------------------------------
        // Input is text, but DSA problems need numbers. Convert carefully.
        Console.Write("Enter a whole number: ");
        string? rawNumber = Console.ReadLine();

        // int.TryParse is the SAFE way to convert:
        //   - returns true  + the parsed value when the text is a valid number
        //   - returns false + 0 when it isn't (no crash!)
        // Compare this with int.Parse(...), which THROWS an exception on bad input.
        if (int.TryParse(rawNumber, out int number))
        {
            Console.WriteLine($"Double of {number} is {number * 2}.");
        }
        else
        {
            Console.WriteLine($"\"{rawNumber}\" is not a valid whole number — that's okay!");
        }

        // --- KEY TAKEAWAYS -------------------------------------------
        Console.WriteLine();
        Console.WriteLine("Remember:");
        Console.WriteLine("  • ReadLine() gives you TEXT, even when the user types digits.");
        Console.WriteLine("  • Use int.TryParse / double.TryParse to convert safely.");
    }
}
