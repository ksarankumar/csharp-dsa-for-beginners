using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 12 — Recursion
///
/// Recursion = a method that calls itself, solving a big problem via smaller copies
/// of the same problem. It underpins trees, graphs, backtracking, and divide-and-conquer.
///
/// Every recursion needs:
///   1. a BASE CASE that stops the recursion
///   2. a RECURSIVE CASE that moves toward the base case
///
/// Goal: factorial, Fibonacci (and why naive Fib is slow), sum of array,
/// power, and a first look at memoization.
/// </summary>
public sealed class Module12_Recursion : ILesson
{
    public int Order => 12;
    public string Title => "Recursion";

    public void Run()
    {
        // --- FACTORIAL -----------------------------------------------
        Console.WriteLine($"5! = {Factorial(5)}  (5 * 4 * 3 * 2 * 1)");

        // --- SUM OF ARRAY --------------------------------------------
        int[] arr = { 1, 2, 3, 4, 5 };
        Console.WriteLine($"Recursive sum = {SumArray(arr, 0)}");

        // --- POWER ---------------------------------------------------
        Console.WriteLine($"2^10 = {Power(2, 10)}");

        // --- FIBONACCI (naive) ---------------------------------------
        // Beautiful but SLOW: fib(n) recomputes the same values exponentially → O(2^n).
        Console.Write("Naive Fibonacci 0..10: ");
        for (int i = 0; i <= 10; i++) Console.Write($"{FibNaive(i)} ");
        Console.WriteLine();

        // --- FIBONACCI (memoized) ------------------------------------
        // Cache results so each value is computed once → O(n). Huge speed-up.
        var memo = new Dictionary<int, long>();
        Console.WriteLine($"Memoized Fibonacci(50) = {FibMemo(50, memo)}");
        Console.WriteLine("(The naive version would take far too long for n = 50!)");

        Console.WriteLine();
        Console.WriteLine("How to think recursively:");
        Console.WriteLine("  1. What's the smallest input I can answer instantly? → base case");
        Console.WriteLine("  2. Assuming the function works for a smaller input, how do I build");
        Console.WriteLine("     the answer for the current input? → recursive case");
        Console.WriteLine("  3. Watch the call stack depth — deep recursion can overflow it.");
    }

    private static long Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);

    private static int SumArray(int[] a, int i) =>
        i == a.Length ? 0 : a[i] + SumArray(a, i + 1);

    private static long Power(int baseNum, int exp) =>
        exp == 0 ? 1 : baseNum * Power(baseNum, exp - 1);

    private static long FibNaive(int n) =>
        n < 2 ? n : FibNaive(n - 1) + FibNaive(n - 2);

    private static long FibMemo(int n, Dictionary<int, long> memo)
    {
        if (n < 2) return n;
        if (memo.TryGetValue(n, out long cached)) return cached;
        long result = FibMemo(n - 1, memo) + FibMemo(n - 2, memo);
        memo[n] = result;
        return result;
    }
}
