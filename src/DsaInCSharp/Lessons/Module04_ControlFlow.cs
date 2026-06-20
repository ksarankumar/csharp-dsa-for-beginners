using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 4 — Control Flow (Loops &amp; Conditionals)
///
/// Every algorithm is built from decisions and repetition.
/// Goal:
///   - if / else if / else, the ternary ?:, and switch (incl. pattern matching)
///   - for, while, do-while, foreach
///   - break, continue, and nested loops
/// </summary>
public sealed class Module04_ControlFlow : ILesson
{
    public int Order => 4;
    public string Title => "Control Flow (Loops & Conditionals)";

    public void Run()
    {
        // --- IF / ELSE -----------------------------------------------
        int score = 82;
        if (score >= 90) Console.WriteLine("Grade: A");
        else if (score >= 80) Console.WriteLine("Grade: B");
        else Console.WriteLine("Grade: C or below");

        // --- TERNARY (a compact if/else that returns a value) --------
        string parity = (score % 2 == 0) ? "even" : "odd";
        Console.WriteLine($"{score} is {parity}");

        // --- SWITCH with pattern matching (modern C#) ----------------
        Console.WriteLine($"Day type: {DescribeDay(6)}");
        Console.WriteLine($"Number bucket: {Classify(-4)}");

        Console.WriteLine();

        // --- FOR LOOP ------------------------------------------------
        Console.Write("for 1..5: ");
        for (int i = 1; i <= 5; i++) Console.Write($"{i} ");
        Console.WriteLine();

        // --- WHILE LOOP (run while a condition holds) ----------------
        Console.Write("while halving 16: ");
        int n = 16;
        while (n >= 1)
        {
            Console.Write($"{n} ");
            n /= 2;
        }
        Console.WriteLine();

        // --- DO-WHILE (always runs at least once) --------------------
        int countdown = 3;
        Console.Write("do-while countdown: ");
        do
        {
            Console.Write($"{countdown} ");
            countdown--;
        } while (countdown > 0);
        Console.WriteLine();

        Console.WriteLine();

        // --- break & continue ----------------------------------------
        Console.Write("first multiple of 7 above 20: ");
        for (int i = 21; ; i++)            // an intentional "infinite" loop...
        {
            if (i % 7 == 0)
            {
                Console.WriteLine(i);
                break;                      // ...exited with break
            }
        }

        Console.Write("odd numbers 1..10 (skip evens): ");
        for (int i = 1; i <= 10; i++)
        {
            if (i % 2 == 0) continue;       // skip the rest of THIS iteration
            Console.Write($"{i} ");
        }
        Console.WriteLine();

        Console.WriteLine();

        // --- NESTED LOOPS (e.g. printing a multiplication grid) ------
        Console.WriteLine("3x3 multiplication grid:");
        for (int r = 1; r <= 3; r++)
        {
            for (int c = 1; c <= 3; c++)
            {
                Console.Write($"{r * c,3}");
            }
            Console.WriteLine();
        }
    }

    // switch expression: concise mapping from input to output.
    private static string DescribeDay(int day) => day switch
    {
        0 or 6 => "weekend",
        >= 1 and <= 5 => "weekday",
        _ => "invalid"           // _ is the default/catch-all
    };

    private static string Classify(int x) => x switch
    {
        < 0 => "negative",
        0 => "zero",
        > 0 => "positive"
    };
}
