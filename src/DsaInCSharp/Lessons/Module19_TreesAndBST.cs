using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 19 — Trees &amp; Binary Search Trees
///
/// A binary tree has nodes with up to two children (left, right). A Binary Search
/// Tree (BST) keeps them ordered: everything left &lt; node &lt; everything right.
/// Trees are everywhere in interviews, and they're naturally recursive.
///
/// Goal: build a BST, the three depth-first traversals (in/pre/post-order),
/// breadth-first (level-order) traversal, height, and BST search.
/// </summary>
public sealed class Module19_TreesAndBST : ILesson
{
    public int Order => 19;
    public string Title => "Trees & Binary Search Trees";

    private sealed class TreeNode
    {
        public int Value;
        public TreeNode? Left;
        public TreeNode? Right;
        public TreeNode(int value) => Value = value;
    }

    public void Run()
    {
        // Build a BST by inserting values.
        TreeNode? root = null;
        foreach (int v in new[] { 5, 3, 8, 1, 4, 7, 9 })
            root = Insert(root, v);

        //         5
        //       /   \
        //      3     8
        //     / \   / \
        //    1   4 7   9

        // --- DEPTH-FIRST TRAVERSALS ----------------------------------
        // In-order on a BST yields SORTED values — a key property to remember.
        var inorder = new List<int>();   InOrder(root, inorder);
        var preorder = new List<int>();  PreOrder(root, preorder);
        var postorder = new List<int>(); PostOrder(root, postorder);
        Console.WriteLine($"In-order   (sorted!): [{string.Join(", ", inorder)}]");
        Console.WriteLine($"Pre-order  (root first): [{string.Join(", ", preorder)}]");
        Console.WriteLine($"Post-order (root last):  [{string.Join(", ", postorder)}]");

        Console.WriteLine();

        // --- BREADTH-FIRST (level-order) -----------------------------
        Console.WriteLine($"Level-order (BFS): [{string.Join(", ", LevelOrder(root))}]");

        Console.WriteLine();

        // --- HEIGHT & SEARCH -----------------------------------------
        Console.WriteLine($"Height: {Height(root)}");
        Console.WriteLine($"Search(7) found? {Search(root, 7)}");
        Console.WriteLine($"Search(6) found? {Search(root, 6)}");

        Console.WriteLine();
        Console.WriteLine("Remember:");
        Console.WriteLine("  • BST in-order traversal → values in sorted order.");
        Console.WriteLine("  • DFS uses recursion/stack; BFS uses a queue, level by level.");
        Console.WriteLine("  • Balanced BST search is O(log n); a skewed one degrades to O(n).");
    }

    // Insert keeping the BST property: smaller goes left, larger goes right.
    private static TreeNode Insert(TreeNode? node, int value)
    {
        if (node is null) return new TreeNode(value);
        if (value < node.Value) node.Left = Insert(node.Left, value);
        else node.Right = Insert(node.Right, value);
        return node;
    }

    private static void InOrder(TreeNode? node, List<int> output)
    {
        if (node is null) return;
        InOrder(node.Left, output);   // left
        output.Add(node.Value);       // node
        InOrder(node.Right, output);  // right
    }

    private static void PreOrder(TreeNode? node, List<int> output)
    {
        if (node is null) return;
        output.Add(node.Value);       // node first
        PreOrder(node.Left, output);
        PreOrder(node.Right, output);
    }

    private static void PostOrder(TreeNode? node, List<int> output)
    {
        if (node is null) return;
        PostOrder(node.Left, output);
        PostOrder(node.Right, output);
        output.Add(node.Value);       // node last
    }

    private static List<int> LevelOrder(TreeNode? root)
    {
        var result = new List<int>();
        if (root is null) return result;
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            TreeNode node = queue.Dequeue();
            result.Add(node.Value);
            if (node.Left is not null) queue.Enqueue(node.Left);
            if (node.Right is not null) queue.Enqueue(node.Right);
        }
        return result;
    }

    private static int Height(TreeNode? node) =>
        node is null ? 0 : 1 + Math.Max(Height(node.Left), Height(node.Right));

    // Exploit the ordering: go left or right instead of checking every node.
    private static bool Search(TreeNode? node, int target)
    {
        if (node is null) return false;
        if (node.Value == target) return true;
        return target < node.Value ? Search(node.Left, target) : Search(node.Right, target);
    }
}
