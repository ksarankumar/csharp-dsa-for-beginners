using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 8 — LINQ Essentials
///
/// LINQ lets you query collections in a readable, declarative way. It's fantastic
/// for prototyping and for the "transform this data" parts of a problem.
///
/// Goal:
///   - Where, Select, OrderBy, Take/Skip
///   - Aggregations: Count, Sum, Min, Max, Average
///   - GroupBy and ToDictionary (frequency maps in one line)
///   - First/Any/All and deferred execution
///   - A note on when NOT to use LINQ in performance-critical inner loops
/// </summary>
public sealed class Module08_Linq : ILesson
{
    public int Order => 8;
    public string Title => "LINQ Essentials";

    public void Run()
    {
        int[] numbers = { 5, 2, 8, 1, 9, 4, 7, 2, 8 };
        Console.WriteLine($"Numbers: [{string.Join(", ", numbers)}]\n");

        // --- FILTER (Where) & TRANSFORM (Select) ---------------------
        var evensSquared = numbers.Where(n => n % 2 == 0).Select(n => n * n);
        Console.WriteLine($"Even numbers squared: [{string.Join(", ", evensSquared)}]");

        // --- ORDER, DISTINCT, TAKE/SKIP ------------------------------
        var topThree = numbers.Distinct().OrderByDescending(n => n).Take(3);
        Console.WriteLine($"Top 3 distinct: [{string.Join(", ", topThree)}]");

        // --- AGGREGATIONS --------------------------------------------
        Console.WriteLine($"Count={numbers.Length}, Sum={numbers.Sum()}, " +
                          $"Min={numbers.Min()}, Max={numbers.Max()}, Avg={numbers.Average():F2}");

        // --- ANY / ALL / FIRST ---------------------------------------
        Console.WriteLine($"Any > 8? {numbers.Any(n => n > 8)}");
        Console.WriteLine($"All > 0? {numbers.All(n => n > 0)}");
        Console.WriteLine($"First even = {numbers.First(n => n % 2 == 0)}");

        Console.WriteLine();

        // --- GROUP BY (build a frequency map declaratively) ----------
        var freq = numbers.GroupBy(n => n)
                          .ToDictionary(g => g.Key, g => g.Count());
        Console.WriteLine("Frequencies:");
        foreach (var kv in freq.OrderBy(kv => kv.Key))
            Console.WriteLine($"  {kv.Key} appears {kv.Value} time(s)");

        Console.WriteLine();

        // --- WORKING WITH OBJECTS ------------------------------------
        var words = new[] { "apple", "banana", "cherry", "date", "fig" };
        var grouped = words.GroupBy(w => w.Length)
                           .OrderBy(g => g.Key);
        Console.WriteLine("Words grouped by length:");
        foreach (var group in grouped)
            Console.WriteLine($"  length {group.Key}: {string.Join(", ", group)}");

        Console.WriteLine();

        // --- DEFERRED EXECUTION (an interview gotcha) ----------------
        // A LINQ query doesn't run until you iterate it. Changing the source first
        // changes the result. Use ToList()/ToArray() to "freeze" it.
        var source = new List<int> { 1, 2, 3 };
        var query = source.Where(n => n > 1);   // not executed yet
        source.Add(4);                          // added BEFORE iteration
        Console.WriteLine($"Deferred query sees the add: [{string.Join(", ", query)}]");

        Console.WriteLine();
        Console.WriteLine("Tip: LINQ is great for clarity, but in tight O(n) inner loops a plain");
        Console.WriteLine("for-loop can be faster and allocates less. Pick the right tool.");
    }
}
