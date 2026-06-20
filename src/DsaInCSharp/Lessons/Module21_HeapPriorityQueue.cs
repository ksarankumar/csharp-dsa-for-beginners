using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 21 — Heaps &amp; Priority Queues
///
/// A heap always gives you the smallest (or largest) item instantly — O(1) to peek,
/// O(log n) to add/remove. C# ships <see cref="PriorityQueue{TElement, TPriority}"/>,
/// a min-heap by priority. Heaps power "top-K", "k-th largest", scheduling, and
/// Dijkstra's algorithm.
///
/// Goal: use PriorityQueue as a min-heap and a max-heap, solve k-th largest and
/// top-K frequent, and merge sorted lists.
/// </summary>
public sealed class Module21_HeapPriorityQueue : ILesson
{
    public int Order => 21;
    public string Title => "Heaps & Priority Queues";

    public void Run()
    {
        // --- MIN-HEAP (default): lowest priority comes out first ------
        var minHeap = new PriorityQueue<string, int>();
        minHeap.Enqueue("low",  1);
        minHeap.Enqueue("high", 10);
        minHeap.Enqueue("mid",  5);
        Console.Write("Min-heap dequeue order: ");
        while (minHeap.Count > 0) Console.Write($"{minHeap.Dequeue()} ");
        Console.WriteLine();

        // --- MAX-HEAP: negate the priority (or use a custom comparer) -
        var maxHeap = new PriorityQueue<string, int>();
        foreach (var (item, p) in new[] { ("low", 1), ("high", 10), ("mid", 5) })
            maxHeap.Enqueue(item, -p);   // negative flips the order
        Console.Write("Max-heap dequeue order: ");
        while (maxHeap.Count > 0) Console.Write($"{maxHeap.Dequeue()} ");
        Console.WriteLine();

        Console.WriteLine();

        // --- K-TH LARGEST (keep a min-heap of size k) ----------------
        // Keep only the k biggest seen so far; the smallest of them is the answer.
        int[] nums = { 3, 2, 1, 5, 6, 4 };
        Console.WriteLine($"3rd largest in [{string.Join(", ", nums)}] = {KthLargest(nums, 3)}");

        Console.WriteLine();

        // --- TOP-K FREQUENT ------------------------------------------
        int[] data = { 1, 1, 1, 2, 2, 3 };
        Console.WriteLine($"Top 2 frequent in [{string.Join(", ", data)}] = " +
                          $"[{string.Join(", ", TopKFrequent(data, 2))}]");

        Console.WriteLine();

        // --- MERGE K SORTED LISTS ------------------------------------
        var lists = new[]
        {
            new[] { 1, 4, 7 },
            new[] { 2, 5, 8 },
            new[] { 3, 6, 9 }
        };
        Console.WriteLine($"Merged sorted: [{string.Join(", ", MergeSortedLists(lists))}]");

        Console.WriteLine();
        Console.WriteLine("When you see 'k largest/smallest', 'top-k', 'k-th', or 'always grab the");
        Console.WriteLine("current best', think heap. Size-k heap → O(n log k), better than sorting.");
    }

    private static int KthLargest(int[] nums, int k)
    {
        var heap = new PriorityQueue<int, int>(); // min-heap of the k largest
        foreach (int n in nums)
        {
            heap.Enqueue(n, n);
            if (heap.Count > k) heap.Dequeue(); // drop the smallest, keep k largest
        }
        return heap.Peek();
    }

    private static int[] TopKFrequent(int[] nums, int k)
    {
        var freq = new Dictionary<int, int>();
        foreach (int n in nums) freq[n] = freq.GetValueOrDefault(n) + 1;

        var heap = new PriorityQueue<int, int>(); // min-heap by frequency
        foreach (var (value, count) in freq.Select(kv => (kv.Key, kv.Value)))
        {
            heap.Enqueue(value, count);
            if (heap.Count > k) heap.Dequeue();
        }

        var result = new int[heap.Count];
        for (int i = result.Length - 1; i >= 0; i--) result[i] = heap.Dequeue();
        return result;
    }

    private static List<int> MergeSortedLists(int[][] lists)
    {
        // Heap holds the current front of each list as (value, listIndex, elementIndex).
        var heap = new PriorityQueue<(int val, int li, int ei), int>();
        for (int i = 0; i < lists.Length; i++)
            if (lists[i].Length > 0)
                heap.Enqueue((lists[i][0], i, 0), lists[i][0]);

        var merged = new List<int>();
        while (heap.Count > 0)
        {
            var (val, li, ei) = heap.Dequeue();
            merged.Add(val);
            if (ei + 1 < lists[li].Length)
            {
                int nextVal = lists[li][ei + 1];
                heap.Enqueue((nextVal, li, ei + 1), nextVal);
            }
        }
        return merged;
    }
}
