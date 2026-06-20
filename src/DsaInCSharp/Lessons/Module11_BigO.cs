using System.Diagnostics;
using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 11 — Big-O &amp; Complexity Analysis
///
/// Big-O describes how an algorithm's work grows as the input grows.
/// Interviewers ALWAYS ask "what's the time/space complexity?" — this lesson
/// builds intuition with real measurements.
///
/// Common classes, fastest → slowest:
///   O(1)        constant      — array index, hash lookup
///   O(log n)    logarithmic   — binary search
///   O(n)        linear        — one pass over the data
///   O(n log n)  linearithmic  — good sorts (MergeSort, QuickSort avg)
///   O(n^2)      quadratic     — nested loops over the same data
///   O(2^n)      exponential   — naive recursion (e.g. plain Fibonacci)
/// </summary>
public sealed class Module11_BigO : ILesson
{
    public int Order => 11;
    public string Title => "Big-O & Complexity Analysis";

    public void Run()
    {
        Console.WriteLine("How the number of operations grows with input size n:\n");
        Console.WriteLine($"|{"n",6}|{"O(1)",8}|{"O(log n)",10}|{"O(n)",8}|{"O(n log n)",12}|{"O(n^2)",10}|");
        Console.WriteLine(new string('-', 60));
        foreach (int n in new[] { 8, 64, 1024, 1_000_000 })
        {
            double logn = Math.Log2(n);
            Console.WriteLine($"|{n,6}|{1,8}|{logn,10:F1}|{n,8}|{n * logn,12:N0}|{(long)n * n,10:N0}|");
        }

        Console.WriteLine();
        Console.WriteLine("Notice O(n^2) explodes while O(log n) barely moves.\n");

        // --- MEASURE IT: O(n) vs O(n^2) ------------------------------
        // Same task (count pairs), two approaches, timed.
        int[] data = new int[5000];
        for (int i = 0; i < data.Length; i++) data[i] = i;

        var sw = Stopwatch.StartNew();
        long linear = SumLinear(data);          // O(n)
        sw.Stop();
        long linearTicks = sw.ElapsedTicks;

        sw.Restart();
        long quadratic = CountPairsQuadratic(data); // O(n^2)
        sw.Stop();
        long quadTicks = sw.ElapsedTicks;

        Console.WriteLine($"O(n)   sum over {data.Length} items   = {linear}  (~{linearTicks} ticks)");
        Console.WriteLine($"O(n^2) pair count over {data.Length} items = {quadratic}  (~{quadTicks} ticks)");
        Console.WriteLine($"The O(n^2) version did roughly {(quadTicks / Math.Max(1, linearTicks))}x more work.");

        Console.WriteLine();
        Console.WriteLine("Rules of thumb when analysing your own code:");
        Console.WriteLine("  • A single loop over n items → O(n).");
        Console.WriteLine("  • A loop inside a loop over n → O(n^2).");
        Console.WriteLine("  • Halving the search space each step → O(log n).");
        Console.WriteLine("  • Drop constants & lower terms: O(2n + 5) is just O(n).");
    }

    private static long SumLinear(int[] a)
    {
        long sum = 0;
        for (int i = 0; i < a.Length; i++) sum += a[i];   // n iterations
        return sum;
    }

    private static long CountPairsQuadratic(int[] a)
    {
        long count = 0;
        for (int i = 0; i < a.Length; i++)
            for (int j = i + 1; j < a.Length; j++)        // n * n / 2 iterations
                count++;
        return count;
    }
}
