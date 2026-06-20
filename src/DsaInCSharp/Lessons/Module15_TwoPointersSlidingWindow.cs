using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 15 — Two Pointers &amp; Sliding Window
///
/// Two of the highest-value patterns on LeetCode. They turn many O(n^2)
/// brute-force solutions into clean O(n) ones.
///
/// Two Pointers: two indices moving through the data (often from both ends, or
/// one slow + one fast).
/// Sliding Window: a moving range [left..right] over a sequence, expanding and
/// shrinking to maintain some property.
/// </summary>
public sealed class Module15_TwoPointersSlidingWindow : ILesson
{
    public int Order => 15;
    public string Title => "Two Pointers & Sliding Window";

    public void Run()
    {
        // --- TWO POINTERS FROM BOTH ENDS -----------------------------
        // Two Sum on a SORTED array: O(n) without a hash map.
        int[] sorted = { 1, 3, 4, 5, 7, 11 };
        (int i, int j) = TwoSumSorted(sorted, 9);
        Console.WriteLine($"TwoSum target 9 → indices ({i},{j}) = values ({sorted[i]},{sorted[j]})");

        // Reverse in place with two pointers.
        int[] toReverse = { 1, 2, 3, 4, 5 };
        ReverseInPlace(toReverse);
        Console.WriteLine($"Reversed in place: [{string.Join(", ", toReverse)}]");

        // Fast/slow pointers: remove duplicates from a sorted array in place.
        int[] dupes = { 1, 1, 2, 2, 2, 3, 4, 4 };
        int newLen = RemoveDuplicates(dupes);
        Console.WriteLine($"After dedupe, first {newLen}: [{string.Join(", ", dupes[..newLen])}]");

        Console.WriteLine();

        // --- SLIDING WINDOW (fixed size) -----------------------------
        // Max sum of any contiguous block of size k — O(n) by sliding the window.
        int[] nums = { 2, 1, 5, 1, 3, 2 };
        Console.WriteLine($"Max sum of 3 consecutive = {MaxSumWindow(nums, 3)}");

        // --- SLIDING WINDOW (variable size) --------------------------
        // Longest substring without repeating characters — a LeetCode classic.
        string s = "abcabcbb";
        Console.WriteLine($"Longest unique substring length of \"{s}\" = {LongestUnique(s)}");

        Console.WriteLine();
        Console.WriteLine("When to reach for these:");
        Console.WriteLine("  • Sorted array + pair/triplet target → two pointers from both ends.");
        Console.WriteLine("  • 'Contiguous subarray/substring' + a constraint → sliding window.");
        Console.WriteLine("  • In-place array editing → slow/fast pointers.");
    }

    private static (int, int) TwoSumSorted(int[] a, int target)
    {
        int lo = 0, hi = a.Length - 1;
        while (lo < hi)
        {
            int sum = a[lo] + a[hi];
            if (sum == target) return (lo, hi);
            if (sum < target) lo++;   // need a bigger sum
            else hi--;                // need a smaller sum
        }
        return (-1, -1);
    }

    private static void ReverseInPlace(int[] a)
    {
        int lo = 0, hi = a.Length - 1;
        while (lo < hi)
        {
            (a[lo], a[hi]) = (a[hi], a[lo]);
            lo++;
            hi--;
        }
    }

    // 'slow' marks the end of the unique section; 'fast' scans ahead.
    private static int RemoveDuplicates(int[] a)
    {
        if (a.Length == 0) return 0;
        int slow = 0;
        for (int fast = 1; fast < a.Length; fast++)
        {
            if (a[fast] != a[slow])
            {
                slow++;
                a[slow] = a[fast];
            }
        }
        return slow + 1;
    }

    private static int MaxSumWindow(int[] a, int k)
    {
        int windowSum = 0;
        for (int i = 0; i < k; i++) windowSum += a[i];   // first window
        int best = windowSum;
        for (int i = k; i < a.Length; i++)
        {
            windowSum += a[i] - a[i - k];                // slide: add new, drop old
            best = Math.Max(best, windowSum);
        }
        return best;
    }

    private static int LongestUnique(string s)
    {
        var seen = new HashSet<char>();
        int left = 0, best = 0;
        for (int right = 0; right < s.Length; right++)
        {
            // Shrink from the left until the new char is unique in the window.
            while (seen.Contains(s[right]))
            {
                seen.Remove(s[left]);
                left++;
            }
            seen.Add(s[right]);
            best = Math.Max(best, right - left + 1);
        }
        return best;
    }
}
