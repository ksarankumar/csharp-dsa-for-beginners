using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 13 — Searching
///
/// Goal:
///   - Linear search: O(n), works on ANY list
///   - Binary search: O(log n), requires a SORTED array — a LeetCode must-know
///   - The half-open template that avoids off-by-one bugs
///   - Binary search "on the answer" (lower-bound style)
/// </summary>
public sealed class Module13_Searching : ILesson
{
    public int Order => 13;
    public string Title => "Searching (Linear & Binary)";

    public void Run()
    {
        int[] sorted = { 1, 3, 5, 7, 9, 11, 13, 15 };
        Console.WriteLine($"Array: [{string.Join(", ", sorted)}]\n");

        // --- LINEAR SEARCH -------------------------------------------
        Console.WriteLine($"LinearSearch(9)  → index {LinearSearch(sorted, 9)}");
        Console.WriteLine($"LinearSearch(8)  → index {LinearSearch(sorted, 8)} (not found)");

        // --- BINARY SEARCH -------------------------------------------
        Console.WriteLine($"BinarySearch(11) → index {BinarySearch(sorted, 11)}");
        Console.WriteLine($"BinarySearch(2)  → index {BinarySearch(sorted, 2)} (not found)");

        // --- BUILT-IN ------------------------------------------------
        // The framework has Array.BinarySearch (returns a negative "complement" if absent).
        Console.WriteLine($"Array.BinarySearch(13) → index {Array.BinarySearch(sorted, 13)}");

        Console.WriteLine();

        // --- LOWER BOUND ---------------------------------------------
        // First index where value >= target. Foundation of many "search the answer" problems.
        Console.WriteLine($"LowerBound(>=6)  → index {LowerBound(sorted, 6)} (value {sorted[LowerBound(sorted, 6)]})");
        Console.WriteLine($"LowerBound(>=7)  → index {LowerBound(sorted, 7)} (value {sorted[LowerBound(sorted, 7)]})");

        Console.WriteLine();
        Console.WriteLine("Why binary search? On 1,000,000 sorted items it needs ~20 steps,");
        Console.WriteLine("while linear search may need 1,000,000. Always confirm the data is sorted!");
    }

    private static int LinearSearch(int[] a, int target)
    {
        for (int i = 0; i < a.Length; i++)
            if (a[i] == target) return i;
        return -1;
    }

    // Classic binary search. Invariant: target, if present, is within [lo, hi].
    private static int BinarySearch(int[] a, int target)
    {
        int lo = 0, hi = a.Length - 1;
        while (lo <= hi)
        {
            int mid = lo + (hi - lo) / 2;     // overflow-safe midpoint
            if (a[mid] == target) return mid;
            if (a[mid] < target) lo = mid + 1; // discard left half
            else hi = mid - 1;                 // discard right half
        }
        return -1;
    }

    // First index i where a[i] >= target (or a.Length if none). Half-open [lo, hi).
    private static int LowerBound(int[] a, int target)
    {
        int lo = 0, hi = a.Length;
        while (lo < hi)
        {
            int mid = lo + (hi - lo) / 2;
            if (a[mid] < target) lo = mid + 1;
            else hi = mid;
        }
        return lo;
    }
}
