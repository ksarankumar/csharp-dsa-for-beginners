using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 5 — Methods &amp; Parameters
///
/// Clean solutions break work into small methods. This lesson covers the
/// parameter features you'll actually use in DSA code:
///   - return values and void
///   - ref, out, and in parameters
///   - params (variable number of arguments)
///   - optional/default parameters
///   - method overloading
///   - local functions (helpers inside a method — great for recursion)
/// </summary>
public sealed class Module05_MethodsAndParameters : ILesson
{
    public int Order => 5;
    public string Title => "Methods & Parameters";

    public void Run()
    {
        // --- BASIC RETURN --------------------------------------------
        Console.WriteLine($"Add(3, 4) = {Add(3, 4)}");

        // --- out: return MORE THAN ONE value -------------------------
        // Common in LeetCode helpers (e.g. return min and max together).
        MinMax(new[] { 5, 1, 9, 3 }, out int min, out int max);
        Console.WriteLine($"min = {min}, max = {max}");

        // --- ref: let a method modify the caller's variable ----------
        int value = 10;
        Double(ref value);
        Console.WriteLine($"After Double(ref): {value}");

        // --- params: pass any number of arguments --------------------
        Console.WriteLine($"Sum() = {Sum()}");
        Console.WriteLine($"Sum(1,2,3,4) = {Sum(1, 2, 3, 4)}");

        // --- optional parameters -------------------------------------
        Console.WriteLine(Greet("Sam"));
        Console.WriteLine(Greet("Sam", "Good evening"));

        // --- overloading: same name, different parameter types -------
        Console.WriteLine($"Area(square) = {Area(5)}");
        Console.WriteLine($"Area(rect)   = {Area(4, 6)}");

        Console.WriteLine();

        // --- LOCAL FUNCTION ------------------------------------------
        // A helper defined INSIDE a method. Perfect for recursion because it can
        // capture local variables and keeps related logic together.
        int Factorial(int k) => k <= 1 ? 1 : k * Factorial(k - 1);
        Console.WriteLine($"Factorial(5) via local function = {Factorial(5)}");

        Console.WriteLine();
        Console.WriteLine("Takeaway: 'out' returns extra values, 'ref' shares a variable,");
        Console.WriteLine("'params' accepts many args, and local functions keep recursion tidy.");
    }

    private static int Add(int a, int b) => a + b;

    private static void MinMax(int[] arr, out int min, out int max)
    {
        min = max = arr[0];
        foreach (int n in arr)
        {
            if (n < min) min = n;
            if (n > max) max = n;
        }
    }

    private static void Double(ref int x) => x *= 2;

    private static int Sum(params int[] values)
    {
        int total = 0;
        foreach (int v in values) total += v;
        return total;
    }

    private static string Greet(string name, string greeting = "Hello") => $"{greeting}, {name}!";

    private static int Area(int side) => side * side;            // overload 1
    private static int Area(int width, int height) => width * height; // overload 2
}
