using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 2 — Basic Data Types
///
/// Goal: understand the building blocks that store information:
///   - Whole numbers (int, long), decimals (double, decimal)
///   - bool, char, string
///   - Value types vs. reference types (a classic interview question)
///   - var, type inference, and overflow
/// </summary>
public sealed class Module02_DataTypes : ILesson
{
    public int Order => 2;
    public string Title => "Basic Data Types";

    public void Run()
    {
        // --- WHOLE NUMBERS -------------------------------------------
        int age = 25;             // 32-bit signed integer: about ±2.1 billion
        long bigNumber = 9_000_000_000; // 64-bit: use when int isn't big enough (_ is just a visual separator)
        Console.WriteLine($"int age = {age}, long bigNumber = {bigNumber}");

        // Every numeric type knows its own limits — useful to avoid overflow bugs.
        Console.WriteLine($"int ranges from {int.MinValue} to {int.MaxValue}");

        // --- DECIMAL NUMBERS -----------------------------------------
        double pi = 3.14159;      // double: fast, good for science/maths, tiny rounding errors
        decimal money = 19.99m;   // decimal: precise base-10, ALWAYS use for currency (note the 'm')
        Console.WriteLine($"double pi = {pi}, decimal money = {money:C}");

        // Why decimal for money? Watch this famous floating-point surprise:
        Console.WriteLine($"0.1 + 0.2 as double  = {0.1 + 0.2}");      // 0.30000000000000004
        Console.WriteLine($"0.1 + 0.2 as decimal = {0.1m + 0.2m}");    // 0.3  ✅

        Console.WriteLine();

        // --- bool, char, string --------------------------------------
        bool isLearning = true;   // only true or false
        char grade = 'A';         // a SINGLE character, single quotes
        string course = "DSA";    // text, double quotes
        Console.WriteLine($"isLearning = {isLearning}, grade = {grade}, course = {course}");

        Console.WriteLine();

        // --- var (TYPE INFERENCE) ------------------------------------
        // 'var' lets the compiler figure out the type from the value on the right.
        // It's still strongly typed — 'score' below is an int forever, just less typing.
        var score = 100;          // compiler infers: int
        var title = "Engineer";   // compiler infers: string
        Console.WriteLine($"var score is an {score.GetType().Name}, title is a {title.GetType().Name}");

        Console.WriteLine();

        // --- VALUE TYPES vs REFERENCE TYPES (interview classic!) -----
        // Value types (int, double, bool, structs) COPY their value on assignment.
        int x = 10;
        int y = x;   // y gets its OWN copy
        y = 99;
        Console.WriteLine($"Value type: x = {x}, y = {y}  (x is unaffected by changing y)");

        // Reference types (arrays, classes, List<T>) copy a REFERENCE to the same object.
        int[] first = { 1, 2, 3 };
        int[] second = first;  // both names point to the SAME array in memory
        second[0] = 99;
        Console.WriteLine($"Reference type: first[0] = {first[0]}  (changed via 'second'!)");

        Console.WriteLine();
        Console.WriteLine("Takeaway: value types copy the data; reference types share the object.");
    }
}
