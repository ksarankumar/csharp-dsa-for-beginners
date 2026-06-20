using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 14 — Sorting
///
/// In real interviews you usually call the built-in sort, then explain how the
/// classic algorithms work. This lesson shows both.
///
/// Goal:
///   - Built-in Array.Sort / List.Sort and custom comparers
///   - Bubble, Selection, Insertion (the simple O(n^2) trio — know how they work)
///   - Merge Sort (O(n log n), stable, divide-and-conquer)
///   - Stability and when it matters
/// </summary>
public sealed class Module14_Sorting : ILesson
{
    public int Order => 14;
    public string Title => "Sorting Algorithms";

    public void Run()
    {
        int[] sample = { 5, 2, 9, 1, 5, 6 };
        Console.WriteLine($"Unsorted: [{string.Join(", ", sample)}]\n");

        Console.WriteLine($"Bubble    → [{string.Join(", ", BubbleSort((int[])sample.Clone()))}]");
        Console.WriteLine($"Selection → [{string.Join(", ", SelectionSort((int[])sample.Clone()))}]");
        Console.WriteLine($"Insertion → [{string.Join(", ", InsertionSort((int[])sample.Clone()))}]");
        Console.WriteLine($"Merge     → [{string.Join(", ", MergeSort((int[])sample.Clone()))}]");

        // --- BUILT-IN ------------------------------------------------
        int[] builtin = (int[])sample.Clone();
        Array.Sort(builtin);                       // highly optimized; use this in real solutions
        Console.WriteLine($"Array.Sort→ [{string.Join(", ", builtin)}]");

        // Custom order: sort DESCENDING with a comparer (lambda).
        int[] desc = (int[])sample.Clone();
        Array.Sort(desc, (x, y) => y.CompareTo(x));
        Console.WriteLine($"Descending→ [{string.Join(", ", desc)}]");

        Console.WriteLine();

        // Sorting objects by a key (common: sort intervals, people by age, etc.).
        var people = new List<(string Name, int Age)>
        {
            ("Alice", 30), ("Bob", 25), ("Cara", 30), ("Dan", 22)
        };
        people.Sort((a, b) => a.Age.CompareTo(b.Age));
        Console.WriteLine("People by age: " + string.Join(", ", people.Select(p => $"{p.Name}({p.Age})")));

        Console.WriteLine();
        Console.WriteLine("Cheat sheet:");
        Console.WriteLine("  • Bubble/Selection/Insertion: O(n^2) — simple, fine for tiny inputs.");
        Console.WriteLine("  • Merge/Quick/Heap: O(n log n) — what you actually use at scale.");
        Console.WriteLine("  • 'Stable' = equal items keep their original relative order.");
    }

    // Repeatedly bubble the largest remaining value to the end.
    private static int[] BubbleSort(int[] a)
    {
        for (int i = 0; i < a.Length - 1; i++)
            for (int j = 0; j < a.Length - 1 - i; j++)
                if (a[j] > a[j + 1])
                    (a[j], a[j + 1]) = (a[j + 1], a[j]); // tuple swap
        return a;
    }

    // Select the smallest remaining element and move it into place.
    private static int[] SelectionSort(int[] a)
    {
        for (int i = 0; i < a.Length - 1; i++)
        {
            int minIdx = i;
            for (int j = i + 1; j < a.Length; j++)
                if (a[j] < a[minIdx]) minIdx = j;
            (a[i], a[minIdx]) = (a[minIdx], a[i]);
        }
        return a;
    }

    // Grow a sorted prefix by inserting each new element into the right spot.
    private static int[] InsertionSort(int[] a)
    {
        for (int i = 1; i < a.Length; i++)
        {
            int key = a[i];
            int j = i - 1;
            while (j >= 0 && a[j] > key)
            {
                a[j + 1] = a[j];
                j--;
            }
            a[j + 1] = key;
        }
        return a;
    }

    // Divide-and-conquer: split in half, sort each half, then merge them.
    private static int[] MergeSort(int[] a)
    {
        if (a.Length <= 1) return a;
        int mid = a.Length / 2;
        int[] left = MergeSort(a[..mid]);    // a[..mid] is a slice (range operator)
        int[] right = MergeSort(a[mid..]);
        return Merge(left, right);
    }

    private static int[] Merge(int[] left, int[] right)
    {
        var result = new int[left.Length + right.Length];
        int i = 0, j = 0, k = 0;
        while (i < left.Length && j < right.Length)
            result[k++] = left[i] <= right[j] ? left[i++] : right[j++];
        while (i < left.Length) result[k++] = left[i++];
        while (j < right.Length) result[k++] = right[j++];
        return result;
    }
}
