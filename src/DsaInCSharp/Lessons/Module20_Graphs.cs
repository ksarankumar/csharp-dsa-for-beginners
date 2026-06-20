using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 20 — Graphs (BFS &amp; DFS)
///
/// A graph is nodes ("vertices") connected by edges. Grids, maps, networks, and
/// dependencies are all graphs. The two fundamental traversals — BFS and DFS —
/// solve a huge range of LeetCode problems.
///
/// Goal: represent a graph with an adjacency list, traverse it with BFS (queue)
/// and DFS (recursion), find shortest unweighted path length with BFS, and count
/// connected components.
/// </summary>
public sealed class Module20_Graphs : ILesson
{
    public int Order => 20;
    public string Title => "Graphs (BFS & DFS)";

    public void Run()
    {
        // Adjacency list: each node maps to its neighbours.
        //   0 - 1 - 2
        //   |       |
        //   3       4
        var graph = new Dictionary<int, List<int>>
        {
            [0] = new() { 1, 3 },
            [1] = new() { 0, 2 },
            [2] = new() { 1, 4 },
            [3] = new() { 0 },
            [4] = new() { 2 }
        };

        // --- BFS (breadth-first, uses a queue) -----------------------
        Console.WriteLine($"BFS from 0: [{string.Join(", ", Bfs(graph, 0))}]");

        // --- DFS (depth-first, uses recursion/stack) -----------------
        var visited = new HashSet<int>();
        var dfsOrder = new List<int>();
        Dfs(graph, 0, visited, dfsOrder);
        Console.WriteLine($"DFS from 0: [{string.Join(", ", dfsOrder)}]");

        Console.WriteLine();

        // --- SHORTEST PATH (unweighted) via BFS ----------------------
        // BFS explores level by level, so the first time we reach a node is the
        // fewest number of edges away.
        Console.WriteLine($"Shortest hops 0→4 = {ShortestPath(graph, 0, 4)}");
        Console.WriteLine($"Shortest hops 3→4 = {ShortestPath(graph, 3, 4)}");

        Console.WriteLine();

        // --- CONNECTED COMPONENTS (grid example) ---------------------
        // Count "islands" of 1s in a grid — a hugely common interview problem.
        int[][] grid =
        {
            new[] { 1, 1, 0, 0 },
            new[] { 1, 0, 0, 1 },
            new[] { 0, 0, 1, 1 },
        };
        Console.WriteLine($"Number of islands = {CountIslands(grid)}");

        Console.WriteLine();
        Console.WriteLine("Choosing a traversal:");
        Console.WriteLine("  • Shortest path in an UNWEIGHTED graph → BFS.");
        Console.WriteLine("  • Explore/flood-fill/all paths, cycle checks → DFS.");
        Console.WriteLine("  • ALWAYS track visited nodes to avoid infinite loops.");
    }

    private static List<int> Bfs(Dictionary<int, List<int>> graph, int start)
    {
        var order = new List<int>();
        var visited = new HashSet<int> { start };
        var queue = new Queue<int>();
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            int node = queue.Dequeue();
            order.Add(node);
            foreach (int next in graph[node])
            {
                if (visited.Add(next))   // Add returns false if already present
                    queue.Enqueue(next);
            }
        }
        return order;
    }

    private static void Dfs(Dictionary<int, List<int>> graph, int node,
                            HashSet<int> visited, List<int> order)
    {
        if (!visited.Add(node)) return;  // already seen → stop
        order.Add(node);
        foreach (int next in graph[node])
            Dfs(graph, next, visited, order);
    }

    private static int ShortestPath(Dictionary<int, List<int>> graph, int start, int target)
    {
        var visited = new HashSet<int> { start };
        var queue = new Queue<(int node, int dist)>();
        queue.Enqueue((start, 0));
        while (queue.Count > 0)
        {
            (int node, int dist) = queue.Dequeue();
            if (node == target) return dist;
            foreach (int next in graph[node])
                if (visited.Add(next))
                    queue.Enqueue((next, dist + 1));
        }
        return -1; // unreachable
    }

    private static int CountIslands(int[][] grid)
    {
        int rows = grid.Length, cols = grid[0].Length, count = 0;

        // Flood-fill sinks an island by turning its 1s into 0s.
        void Sink(int r, int c)
        {
            if (r < 0 || r >= rows || c < 0 || c >= cols || grid[r][c] == 0) return;
            grid[r][c] = 0;
            Sink(r + 1, c); Sink(r - 1, c);
            Sink(r, c + 1); Sink(r, c - 1);
        }

        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                if (grid[r][c] == 1)
                {
                    count++;
                    Sink(r, c);
                }
        return count;
    }
}
