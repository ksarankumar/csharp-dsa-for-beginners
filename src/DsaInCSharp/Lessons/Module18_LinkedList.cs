using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 18 — Linked Lists
///
/// A linked list is a chain of nodes, each holding a value and a reference to the
/// next node. Unlike arrays, there's no index — you follow the chain. Building one
/// yourself teaches pointers/references, which interviewers love to test.
///
/// Goal: build a singly linked list, traverse it, and master the three classics:
///   - reverse a linked list
///   - detect a cycle (Floyd's fast/slow pointers)
///   - find the middle node
/// </summary>
public sealed class Module18_LinkedList : ILesson
{
    public int Order => 18;
    public string Title => "Linked Lists";

    // A node: one value + a link to the next node (null means end of list).
    private sealed class Node
    {
        public int Value;
        public Node? Next;
        public Node(int value) => Value = value;
    }

    public void Run()
    {
        // --- BUILD from an array -------------------------------------
        Node? head = Build(new[] { 1, 2, 3, 4, 5 });
        Console.WriteLine($"List:      {ToText(head)}");

        // --- TRAVERSE / COUNT ----------------------------------------
        Console.WriteLine($"Length:    {Length(head)}");

        // --- FIND MIDDLE (fast/slow) ---------------------------------
        Console.WriteLine($"Middle:    {FindMiddle(head)?.Value}");

        // --- REVERSE -------------------------------------------------
        head = Reverse(head);
        Console.WriteLine($"Reversed:  {ToText(head)}");

        Console.WriteLine();

        // --- CYCLE DETECTION -----------------------------------------
        Node? a = Build(new[] { 10, 20, 30 });
        Console.WriteLine($"HasCycle(straight list) = {HasCycle(a)}");

        // Manually create a cycle: tail points back to the head.
        Node cyc1 = new(1), cyc2 = new(2), cyc3 = new(3);
        cyc1.Next = cyc2; cyc2.Next = cyc3; cyc3.Next = cyc1; // loop!
        Console.WriteLine($"HasCycle(looped list)   = {HasCycle(cyc1)}");

        Console.WriteLine();
        Console.WriteLine("Array vs linked list:");
        Console.WriteLine("  • Array: O(1) index access, but inserting in the middle shifts items.");
        Console.WriteLine("  • Linked list: O(1) insert/remove given the node, but O(n) to FIND it.");
        Console.WriteLine("  • Fast/slow pointers solve middle-finding and cycle-detection elegantly.");
    }

    private static Node? Build(int[] values)
    {
        Node dummy = new(0);          // a throwaway head simplifies the building loop
        Node tail = dummy;
        foreach (int v in values)
        {
            tail.Next = new Node(v);
            tail = tail.Next;
        }
        return dummy.Next;
    }

    private static int Length(Node? head)
    {
        int count = 0;
        for (Node? cur = head; cur is not null; cur = cur.Next) count++;
        return count;
    }

    // Reverse by re-pointing each node's Next to the previous node.
    private static Node? Reverse(Node? head)
    {
        Node? prev = null;
        Node? cur = head;
        while (cur is not null)
        {
            Node? next = cur.Next; // remember the rest of the list
            cur.Next = prev;       // flip the link
            prev = cur;            // advance prev
            cur = next;            // advance cur
        }
        return prev;               // new head
    }

    // Slow moves 1 step, fast moves 2. When fast reaches the end, slow is at the middle.
    private static Node? FindMiddle(Node? head)
    {
        Node? slow = head, fast = head;
        while (fast?.Next is not null)
        {
            slow = slow!.Next;
            fast = fast.Next.Next;
        }
        return slow;
    }

    // Floyd's algorithm: if there's a loop, the fast pointer eventually laps the slow one.
    private static bool HasCycle(Node? head)
    {
        Node? slow = head, fast = head;
        while (fast?.Next is not null)
        {
            slow = slow!.Next;
            fast = fast.Next.Next;
            if (ReferenceEquals(slow, fast)) return true;
        }
        return false;
    }

    private static string ToText(Node? head)
    {
        var parts = new List<string>();
        for (Node? cur = head; cur is not null; cur = cur.Next) parts.Add(cur.Value.ToString());
        return parts.Count == 0 ? "(empty)" : string.Join(" -> ", parts) + " -> null";
    }
}
