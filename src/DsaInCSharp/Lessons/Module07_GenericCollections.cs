using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 7 — Generic Collections
///
/// Goal: meet the workhorse collections you'll use in nearly every DSA problem,
/// and learn WHEN to reach for each one:
///   - List&lt;T&gt;                 → a resizable array
///   - Dictionary&lt;TKey,TValue&gt; → fast key → value lookups (hash map)
///   - HashSet&lt;T&gt;              → unique items, fast "does it contain?" checks
///   - Stack&lt;T&gt; / Queue&lt;T&gt;    → LIFO and FIFO ordering
///
/// "Generic" means &lt;T&gt;: the collection works with ANY type you choose
/// (List&lt;int&gt;, List&lt;string&gt;, ...) while staying type-safe and fast.
/// </summary>
public sealed class Module07_GenericCollections : ILesson
{
    public int Order => 7;
    public string Title => "Generic Collections";

    public void Run()
    {
        ListDemo();
        Console.WriteLine();
        DictionaryDemo();
        Console.WriteLine();
        HashSetDemo();
        Console.WriteLine();
        StackAndQueueDemo();

        Console.WriteLine();
        Console.WriteLine("Rule of thumb:");
        Console.WriteLine("  • Ordered, index access      → List<T>");
        Console.WriteLine("  • Look up by key             → Dictionary<TKey,TValue>");
        Console.WriteLine("  • Uniqueness / membership    → HashSet<T>");
        Console.WriteLine("  • Last-in-first-out          → Stack<T>");
        Console.WriteLine("  • First-in-first-out         → Queue<T>");
    }

    // List<T>: like an array that grows and shrinks for you.
    private static void ListDemo()
    {
        Console.WriteLine("── List<T> ───────────────────────────────");
        List<string> fruits = new() { "apple", "banana" };
        fruits.Add("cherry");              // add to the end
        fruits.Insert(0, "avocado");       // insert at a specific index
        fruits.Remove("banana");           // remove by value

        Console.WriteLine($"Count: {fruits.Count}, first item: {fruits[0]}");
        Console.WriteLine("Items: " + string.Join(", ", fruits));
    }

    // Dictionary<TKey, TValue>: store and retrieve values by a key. Lookups are ~O(1).
    private static void DictionaryDemo()
    {
        Console.WriteLine("── Dictionary<TKey,TValue> ───────────────");
        Dictionary<string, int> ages = new()
        {
            ["Alice"] = 30,
            ["Bob"] = 25
        };
        ages["Charlie"] = 28;

        // TryGetValue is the safe lookup — no crash if the key is missing.
        if (ages.TryGetValue("Alice", out int aliceAge))
        {
            Console.WriteLine($"Alice is {aliceAge}.");
        }

        Console.WriteLine($"Do we know Dave? {ages.ContainsKey("Dave")}");
        foreach (KeyValuePair<string, int> entry in ages)
        {
            Console.WriteLine($"  {entry.Key} → {entry.Value}");
        }
    }

    // HashSet<T>: a collection of UNIQUE items with very fast Contains checks.
    private static void HashSetDemo()
    {
        Console.WriteLine("── HashSet<T> ────────────────────────────");
        HashSet<int> numbers = new() { 1, 2, 2, 3, 3, 3 }; // duplicates are ignored
        numbers.Add(4);
        bool added = numbers.Add(4); // false — 4 is already present

        Console.WriteLine($"Unique values: {string.Join(", ", numbers)}");
        Console.WriteLine($"Adding 4 again succeeded? {added}");
        Console.WriteLine($"Contains 3? {numbers.Contains(3)}");
    }

    // Stack<T> (LIFO) and Queue<T> (FIFO): order matters in many algorithms.
    private static void StackAndQueueDemo()
    {
        Console.WriteLine("── Stack<T> (LIFO) & Queue<T> (FIFO) ─────");

        // Stack: think of a stack of plates — last one on is first one off.
        Stack<string> stack = new();
        stack.Push("first");
        stack.Push("second");
        stack.Push("third");
        Console.WriteLine($"Stack pops: {stack.Pop()}, {stack.Pop()}"); // third, second

        // Queue: think of a line at a counter — first to arrive is first served.
        Queue<string> queue = new();
        queue.Enqueue("first");
        queue.Enqueue("second");
        queue.Enqueue("third");
        Console.WriteLine($"Queue dequeues: {queue.Dequeue()}, {queue.Dequeue()}"); // first, second
    }
}
