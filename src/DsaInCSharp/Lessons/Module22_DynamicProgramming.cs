using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 22 — Dynamic Programming (DP)
///
/// DP solves problems by combining answers to overlapping sub-problems, and
/// REMEMBERING those answers so they're computed once. Two flavours:
///   - Top-down (memoization): recursion + a cache.
///   - Bottom-up (tabulation): fill a table from the smallest cases upward.
///
/// Goal: climbing stairs, coin change, longest common subsequence, and the
/// 0/1 knapsack — the templates behind countless LeetCode "DP" problems.
/// </summary>
public sealed class Module22_DynamicProgramming : ILesson
{
    public int Order => 22;
    public string Title => "Dynamic Programming";

    public void Run()
    {
        // --- CLIMBING STAIRS -----------------------------------------
        // Ways to reach step n taking 1 or 2 steps. ways[n] = ways[n-1] + ways[n-2].
        Console.WriteLine($"ClimbStairs(5) = {ClimbStairs(5)} distinct ways");

        // --- COIN CHANGE (fewest coins) ------------------------------
        int[] coins = { 1, 2, 5 };
        Console.WriteLine($"Fewest coins for 11 from [1,2,5] = {CoinChange(coins, 11)}");
        Console.WriteLine($"Fewest coins for 3 from [2]     = {CoinChange(new[] { 2 }, 3)} (-1 = impossible)");

        Console.WriteLine();

        // --- LONGEST COMMON SUBSEQUENCE ------------------------------
        Console.WriteLine($"LCS(\"abcde\",\"ace\") length = {LongestCommonSubsequence("abcde", "ace")}");

        Console.WriteLine();

        // --- 0/1 KNAPSACK --------------------------------------------
        int[] weights = { 1, 3, 4, 5 };
        int[] values  = { 1, 4, 5, 7 };
        Console.WriteLine($"Knapsack (capacity 7) max value = {Knapsack(weights, values, 7)}");

        Console.WriteLine();
        Console.WriteLine("How to spot DP:");
        Console.WriteLine("  • 'Count the ways', 'min/max cost', 'is it possible' over choices.");
        Console.WriteLine("  • The brute-force recursion recomputes the same sub-problems.");
        Console.WriteLine("Recipe: define the state, write the recurrence, add a base case,");
        Console.WriteLine("then cache (memoize) or build a table (tabulate).");
    }

    // Bottom-up; this is just Fibonacci in disguise.
    private static int ClimbStairs(int n)
    {
        if (n <= 2) return n;
        int prev = 1, curr = 2;
        for (int i = 3; i <= n; i++)
        {
            int next = prev + curr;
            prev = curr;
            curr = next;
        }
        return curr;
    }

    // dp[a] = fewest coins to make amount a. Try every coin for every amount.
    private static int CoinChange(int[] coins, int amount)
    {
        int[] dp = new int[amount + 1];
        Array.Fill(dp, amount + 1);   // "infinity" sentinel
        dp[0] = 0;
        for (int a = 1; a <= amount; a++)
            foreach (int coin in coins)
                if (coin <= a)
                    dp[a] = Math.Min(dp[a], dp[a - coin] + 1);
        return dp[amount] > amount ? -1 : dp[amount];
    }

    // dp[i,j] = LCS length of a[..i] and b[..j].
    private static int LongestCommonSubsequence(string a, string b)
    {
        int[,] dp = new int[a.Length + 1, b.Length + 1];
        for (int i = 1; i <= a.Length; i++)
            for (int j = 1; j <= b.Length; j++)
                dp[i, j] = a[i - 1] == b[j - 1]
                    ? dp[i - 1, j - 1] + 1                          // chars match → extend
                    : Math.Max(dp[i - 1, j], dp[i, j - 1]);        // skip one char
        return dp[a.Length, b.Length];
    }

    // dp[c] = best value achievable with capacity c. Iterate capacity DOWNWARD
    // so each item is used at most once (the "0/1" constraint).
    private static int Knapsack(int[] weights, int[] values, int capacity)
    {
        int[] dp = new int[capacity + 1];
        for (int i = 0; i < weights.Length; i++)
            for (int c = capacity; c >= weights[i]; c--)
                dp[c] = Math.Max(dp[c], dp[c - weights[i]] + values[i]);
        return dp[capacity];
    }
}
