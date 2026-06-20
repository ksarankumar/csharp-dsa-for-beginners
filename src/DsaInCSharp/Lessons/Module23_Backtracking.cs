using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 23 — Backtracking
///
/// Backtracking explores all candidate solutions by building them one choice at a
/// time and "undoing" a choice when it can't lead to a valid answer. It's the go-to
/// approach for permutations, combinations, subsets, and constraint puzzles.
///
/// The template:
///   choose → explore (recurse) → un-choose (backtrack)
///
/// Goal: generate subsets, permutations, combinations, and letter combinations
/// of a phone number.
/// </summary>
public sealed class Module23_Backtracking : ILesson
{
    public int Order => 23;
    public string Title => "Backtracking";

    public void Run()
    {
        // --- SUBSETS (the power set) ---------------------------------
        var subsets = Subsets(new[] { 1, 2, 3 });
        Console.WriteLine($"Subsets of [1,2,3] ({subsets.Count}):");
        Console.WriteLine("  " + string.Join("  ", subsets.Select(s => $"[{string.Join(",", s)}]")));

        Console.WriteLine();

        // --- PERMUTATIONS --------------------------------------------
        var perms = Permutations(new[] { 1, 2, 3 });
        Console.WriteLine($"Permutations of [1,2,3] ({perms.Count}):");
        Console.WriteLine("  " + string.Join("  ", perms.Select(p => $"[{string.Join(",", p)}]")));

        Console.WriteLine();

        // --- COMBINATIONS (choose k of n) ----------------------------
        var combos = Combinations(4, 2);
        Console.WriteLine($"Combinations C(4,2) ({combos.Count}):");
        Console.WriteLine("  " + string.Join("  ", combos.Select(c => $"[{string.Join(",", c)}]")));

        Console.WriteLine();

        // --- PHONE LETTER COMBINATIONS -------------------------------
        var letters = LetterCombinations("23");
        Console.WriteLine($"Letter combos for \"23\" ({letters.Count}): {string.Join(", ", letters)}");

        Console.WriteLine();
        Console.WriteLine("Backtracking template to memorize:");
        Console.WriteLine("  void Backtrack(state):");
        Console.WriteLine("    if (complete) record a copy of the solution; return;");
        Console.WriteLine("    for each choice:");
        Console.WriteLine("       apply choice  →  Backtrack(...)  →  undo choice");
        Console.WriteLine("Tip: always record a COPY of the path, since you mutate it as you go.");
    }

    private static List<List<int>> Subsets(int[] nums)
    {
        var result = new List<List<int>>();
        var path = new List<int>();

        void Backtrack(int start)
        {
            result.Add(new List<int>(path));     // every node in the tree is a subset
            for (int i = start; i < nums.Length; i++)
            {
                path.Add(nums[i]);               // choose
                Backtrack(i + 1);                // explore
                path.RemoveAt(path.Count - 1);   // un-choose
            }
        }

        Backtrack(0);
        return result;
    }

    private static List<List<int>> Permutations(int[] nums)
    {
        var result = new List<List<int>>();
        var path = new List<int>();
        var used = new bool[nums.Length];

        void Backtrack()
        {
            if (path.Count == nums.Length)
            {
                result.Add(new List<int>(path));
                return;
            }
            for (int i = 0; i < nums.Length; i++)
            {
                if (used[i]) continue;
                used[i] = true; path.Add(nums[i]);
                Backtrack();
                used[i] = false; path.RemoveAt(path.Count - 1);
            }
        }

        Backtrack();
        return result;
    }

    private static List<List<int>> Combinations(int n, int k)
    {
        var result = new List<List<int>>();
        var path = new List<int>();

        void Backtrack(int start)
        {
            if (path.Count == k)
            {
                result.Add(new List<int>(path));
                return;
            }
            for (int i = start; i <= n; i++)
            {
                path.Add(i);
                Backtrack(i + 1);
                path.RemoveAt(path.Count - 1);
            }
        }

        Backtrack(1);
        return result;
    }

    private static List<string> LetterCombinations(string digits)
    {
        var result = new List<string>();
        if (digits.Length == 0) return result;
        string[] map = { "", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };
        var path = new char[digits.Length];

        void Backtrack(int index)
        {
            if (index == digits.Length)
            {
                result.Add(new string(path));
                return;
            }
            foreach (char c in map[digits[index] - '0'])
            {
                path[index] = c;
                Backtrack(index + 1);
            }
        }

        Backtrack(0);
        return result;
    }
}
