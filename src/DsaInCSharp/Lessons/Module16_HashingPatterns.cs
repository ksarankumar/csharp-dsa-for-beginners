using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 16 — Hashing Patterns
///
/// Hash maps (Dictionary) and hash sets turn "search the whole list" — O(n) —
/// into near-instant O(1) lookups. This single idea unlocks a massive number of
/// LeetCode problems.
///
/// Goal: frequency counting, the Two Sum hash trick, anagram check,
/// first unique character, and grouping by a computed key.
/// </summary>
public sealed class Module16_HashingPatterns : ILesson
{
    public int Order => 16;
    public string Title => "Hashing Patterns";

    public void Run()
    {
        // --- FREQUENCY MAP -------------------------------------------
        // The single most reused pattern: count how often each item appears.
        int[] nums = { 1, 2, 2, 3, 3, 3, 4 };
        var counts = new Dictionary<int, int>();
        foreach (int n in nums)
        {
            // GetValueOrDefault returns 0 if the key isn't present yet.
            counts[n] = counts.GetValueOrDefault(n) + 1;
        }
        Console.WriteLine("Frequencies: " + string.Join(", ", counts.Select(kv => $"{kv.Key}×{kv.Value}")));

        Console.WriteLine();

        // --- TWO SUM with a hash map (unsorted, O(n)) ----------------
        // For each number, check if its "complement" (target - n) was already seen.
        int[] arr = { 3, 8, 2, 5, 11 };
        (int i, int j) = TwoSum(arr, 10);
        Console.WriteLine($"TwoSum target 10 → indices ({i},{j}) = ({arr[i]},{arr[j]})");

        Console.WriteLine();

        // --- ANAGRAM CHECK -------------------------------------------
        Console.WriteLine($"IsAnagram(\"listen\",\"silent\") = {IsAnagram("listen", "silent")}");
        Console.WriteLine($"IsAnagram(\"hello\",\"world\")   = {IsAnagram("hello", "world")}");

        Console.WriteLine();

        // --- FIRST UNIQUE CHARACTER ----------------------------------
        Console.WriteLine($"FirstUnique(\"leetcode\") index = {FirstUniqueChar("leetcode")}");
        Console.WriteLine($"FirstUnique(\"aabb\")     index = {FirstUniqueChar("aabb")} (none)");

        Console.WriteLine();

        // --- GROUP ANAGRAMS (key = sorted letters) -------------------
        string[] words = { "eat", "tea", "tan", "ate", "nat", "bat" };
        var groups = GroupAnagrams(words);
        Console.WriteLine("Grouped anagrams:");
        foreach (var g in groups) Console.WriteLine($"  [{string.Join(", ", g)}]");

        Console.WriteLine();
        Console.WriteLine("Signal to use hashing: you're repeatedly asking 'have I seen X?' or");
        Console.WriteLine("'how many of X?'. A Dictionary/HashSet usually makes it O(n).");
    }

    private static (int, int) TwoSum(int[] a, int target)
    {
        var seen = new Dictionary<int, int>(); // value → index
        for (int i = 0; i < a.Length; i++)
        {
            int need = target - a[i];
            if (seen.TryGetValue(need, out int j)) return (j, i);
            seen[a[i]] = i;
        }
        return (-1, -1);
    }

    private static bool IsAnagram(string a, string b)
    {
        if (a.Length != b.Length) return false;
        var count = new Dictionary<char, int>();
        foreach (char c in a) count[c] = count.GetValueOrDefault(c) + 1;
        foreach (char c in b)
        {
            if (!count.ContainsKey(c)) return false;
            count[c]--;
            if (count[c] == 0) count.Remove(c);
        }
        return count.Count == 0;
    }

    private static int FirstUniqueChar(string s)
    {
        var freq = new Dictionary<char, int>();
        foreach (char c in s) freq[c] = freq.GetValueOrDefault(c) + 1;
        for (int i = 0; i < s.Length; i++)
            if (freq[s[i]] == 1) return i;
        return -1;
    }

    private static List<List<string>> GroupAnagrams(string[] words)
    {
        var map = new Dictionary<string, List<string>>();
        foreach (string w in words)
        {
            char[] chars = w.ToCharArray();
            Array.Sort(chars);
            string key = new(chars);              // sorted letters identify an anagram group
            if (!map.TryGetValue(key, out var list))
            {
                list = new List<string>();
                map[key] = list;
            }
            list.Add(w);
        }
        return map.Values.ToList();
    }
}
