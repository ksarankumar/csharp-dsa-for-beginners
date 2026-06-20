using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 17 — Stack &amp; Queue Patterns
///
/// You met Stack&lt;T&gt; and Queue&lt;T&gt; in Lesson 4. Here we use them to solve
/// real problem patterns.
///
/// Stack (LIFO) shines for "matching"/"undo"/"most recent" problems.
/// Queue (FIFO) shines for "process in arrival order" and BFS.
///
/// Goal: valid parentheses, next greater element (monotonic stack),
/// evaluate Reverse Polish Notation, and a queue-based moving average.
/// </summary>
public sealed class Module17_StackQueuePatterns : ILesson
{
    public int Order => 17;
    public string Title => "Stack & Queue Patterns";

    public void Run()
    {
        // --- VALID PARENTHESES (stack matching) ----------------------
        foreach (string test in new[] { "()[]{}", "([{}])", "(]", "(()" })
            Console.WriteLine($"IsValid(\"{test}\") = {IsValidParentheses(test)}");

        Console.WriteLine();

        // --- NEXT GREATER ELEMENT (monotonic stack) ------------------
        // For each element, find the next element to its right that is bigger.
        int[] nums = { 2, 1, 2, 4, 3 };
        int[] nge = NextGreater(nums);
        Console.WriteLine($"Array:        [{string.Join(", ", nums)}]");
        Console.WriteLine($"Next greater: [{string.Join(", ", nge)}]  (-1 = none)");

        Console.WriteLine();

        // --- EVALUATE REVERSE POLISH NOTATION ------------------------
        // "3 4 + 2 *" means (3 + 4) * 2 = 14. Operators pop two operands.
        string[] rpn = { "3", "4", "+", "2", "*" };
        Console.WriteLine($"RPN [{string.Join(" ", rpn)}] = {EvalRPN(rpn)}");

        Console.WriteLine();

        // --- QUEUE: MOVING AVERAGE -----------------------------------
        var ma = new MovingAverage(3);
        Console.Write("Moving average (window 3): ");
        foreach (int v in new[] { 1, 10, 3, 5 })
            Console.Write($"{ma.Next(v):F2} ");
        Console.WriteLine();

        Console.WriteLine();
        Console.WriteLine("Pattern hints:");
        Console.WriteLine("  • Brackets, undo, 'most recent unmatched' → Stack.");
        Console.WriteLine("  • 'Next greater/smaller', spans → monotonic Stack.");
        Console.WriteLine("  • Process oldest first, BFS, sliding queue → Queue.");
    }

    private static bool IsValidParentheses(string s)
    {
        var stack = new Stack<char>();
        var pairs = new Dictionary<char, char> { [')'] = '(', [']'] = '[', ['}'] = '{' };
        foreach (char c in s)
        {
            if (c is '(' or '[' or '{')
            {
                stack.Push(c);
            }
            else
            {
                // A closing bracket must match the most recent opening one.
                if (stack.Count == 0 || stack.Pop() != pairs[c]) return false;
            }
        }
        return stack.Count == 0;
    }

    private static int[] NextGreater(int[] a)
    {
        int[] result = new int[a.Length];
        Array.Fill(result, -1);
        var stack = new Stack<int>(); // holds INDICES whose answer we haven't found yet
        for (int i = 0; i < a.Length; i++)
        {
            // Current value resolves every smaller value waiting on the stack.
            while (stack.Count > 0 && a[i] > a[stack.Peek()])
                result[stack.Pop()] = a[i];
            stack.Push(i);
        }
        return result;
    }

    private static int EvalRPN(string[] tokens)
    {
        var stack = new Stack<int>();
        foreach (string t in tokens)
        {
            if (int.TryParse(t, out int num))
            {
                stack.Push(num);
            }
            else
            {
                int b = stack.Pop();
                int a = stack.Pop();
                stack.Push(t switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => a / b,
                    _ => throw new ArgumentException($"Unknown operator {t}")
                });
            }
        }
        return stack.Pop();
    }

    // A small class that keeps the last 'size' values in a queue.
    private sealed class MovingAverage
    {
        private readonly Queue<int> _window = new();
        private readonly int _size;
        private int _sum;

        public MovingAverage(int size) => _size = size;

        public double Next(int value)
        {
            _window.Enqueue(value);
            _sum += value;
            if (_window.Count > _size) _sum -= _window.Dequeue(); // drop the oldest
            return (double)_sum / _window.Count;
        }
    }
}
