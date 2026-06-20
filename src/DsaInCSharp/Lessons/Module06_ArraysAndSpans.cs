using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 6 — Arrays &amp; Spans
///
/// Arrays are THE most common structure in LeetCode problems.
/// Goal:
///   - Declare, fill, and iterate arrays (1-D and 2-D)
///   - Common operations: reverse, copy, search, sort
///   - jagged arrays vs multi-dimensional arrays
///   - a peek at Span&lt;T&gt; (zero-copy slicing — fast and interview-worthy)
/// </summary>
public sealed class Module06_ArraysAndSpans : ILesson
{
    public int Order => 6;
    public string Title => "Arrays & Spans";

    public void Run()
    {
        // --- DECLARING ARRAYS ----------------------------------------
        int[] numbers = { 5, 2, 9, 1, 7 };          // fixed size, set at creation
        int[] zeros = new int[4];                   // all elements default to 0
        Console.WriteLine($"numbers length = {numbers.Length}, zeros = [{string.Join(", ", zeros)}]");

        // --- ITERATING -----------------------------------------------
        // Classic for-loop: gives you the index, essential for many algorithms.
        int sum = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            sum += numbers[i];
        }
        Console.WriteLine($"Sum via for-loop = {sum}");

        // foreach: cleaner when you don't need the index.
        int max = int.MinValue;
        foreach (int n in numbers)
        {
            if (n > max) max = n;
        }
        Console.WriteLine($"Max via foreach = {max}");

        // --- COMMON OPERATIONS ---------------------------------------
        int[] copy = (int[])numbers.Clone();        // independent copy
        Array.Sort(copy);                            // in-place ascending sort
        Console.WriteLine($"Sorted copy = [{string.Join(", ", copy)}]");
        Console.WriteLine($"Original unchanged = [{string.Join(", ", numbers)}]");

        Array.Reverse(copy);
        Console.WriteLine($"Reversed = [{string.Join(", ", copy)}]");

        int idx = Array.IndexOf(numbers, 9);         // first index of value, or -1
        Console.WriteLine($"Value 9 is at index {idx}");

        Console.WriteLine();

        // --- 2-D GRID (rectangular array) ----------------------------
        // Great for matrix problems (grids, islands, dynamic programming tables).
        int[,] grid =
        {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };
        Console.WriteLine($"grid rows = {grid.GetLength(0)}, cols = {grid.GetLength(1)}");
        for (int r = 0; r < grid.GetLength(0); r++)
        {
            for (int c = 0; c < grid.GetLength(1); c++)
            {
                Console.Write($"{grid[r, c]} ");
            }
            Console.WriteLine();
        }

        Console.WriteLine();

        // --- JAGGED ARRAY (array of arrays, rows can differ in length) ---
        int[][] jagged =
        {
            new[] { 1 },
            new[] { 2, 3 },
            new[] { 4, 5, 6 }
        };
        Console.WriteLine("Jagged rows:");
        foreach (int[] row in jagged)
        {
            Console.WriteLine("  " + string.Join(", ", row));
        }

        Console.WriteLine();

        // --- SPAN<T>: a window into an array with NO copying ----------
        // Slicing an array normally copies; a Span just "looks at" part of it — very efficient.
        int[] data = { 10, 20, 30, 40, 50 };
        Span<int> middle = data.AsSpan(1, 3);        // start at index 1, take 3 items
        middle[0] = 999;                              // writing through the span edits the original!
        Console.WriteLine($"After span edit, data = [{string.Join(", ", data)}]");

        Console.WriteLine();
        Console.WriteLine("Takeaway: arrays are fixed-size; use index loops for algorithms,");
        Console.WriteLine("and reach for Span<T> when you want fast slices without copying.");
    }
}
