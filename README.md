# DSA in C# — The Beginning

A **structured, beginner-to-interview** C# console course you can learn one module at a time.
Every topic lives in its own self-contained lesson with heavy, plain-English comments — so you
can read the code *and* understand the *why* behind it. Built for anyone starting the journey
toward **C# / Data-Structures-&-Algorithms (LeetCode-style) interviews**.

> Pick a number from the menu, watch the concept run live in the terminal, then open the
> matching file to read the explanation line by line.

---

## ▶️ How to run it

You need the [.NET SDK](https://dotnet.microsoft.com/download) (this project targets **.NET 10**).

```bash
# from the repository root
dotnet run --project src/DsaInCSharp
```

You'll see an interactive menu. Type a lesson number and press Enter. Press `q` to quit.

---

## 🎯 Who this is for

- You know a little programming and want to learn **C# specifically for DSA interviews**.
- You prefer **learning by reading runnable, commented code** over walls of theory.
- You want a **clear path**: language basics → complexity → patterns → data structures → algorithms.

---

## 📚 Curriculum

Work through them in order — each builds on the last.

### Part 1 — C# language fundamentals
| #  | Topic | What you'll learn |
|----|-------|-------------------|
| 1  | Input / Output Basics              | `WriteLine`, `ReadLine`, safe `int.TryParse` |
| 2  | Basic Data Types                   | `int`/`double`/`decimal`, `bool`/`char`/`string`, value vs reference types |
| 3  | String Interpolation & Formatting  | `$"..."`, number/currency formats, alignment, raw strings |
| 4  | Control Flow                       | `if`/`switch` patterns, `for`/`while`/`do`, `break`/`continue` |
| 5  | Methods & Parameters               | `ref`/`out`/`params`, overloading, local functions |

### Part 2 — Built-in data containers
| #  | Topic | What you'll learn |
|----|-------|-------------------|
| 6  | Arrays & Spans                     | 1-D/2-D/jagged arrays, common ops, `Span<T>` slicing |
| 7  | Generic Collections                | `List`, `Dictionary`, `HashSet`, `Stack`, `Queue` |
| 8  | LINQ Essentials                    | `Where`/`Select`/`GroupBy`, aggregations, deferred execution |

### Part 3 — Working with text & numbers
| #  | Topic | What you'll learn |
|----|-------|-------------------|
| 9  | String Manipulation                | immutability, `StringBuilder`, char tricks, palindrome |
| 10 | Math & Bit Manipulation            | modulo, overflow-safe mid, bitwise ops, XOR tricks |

### Part 4 — Efficiency & core algorithms
| #  | Topic | What you'll learn |
|----|-------|-------------------|
| 11 | Big-O & Complexity Analysis        | O(1)…O(n²), measured live, how to analyze your code |
| 12 | Recursion                          | base/recursive cases, **memoization** |
| 13 | Searching                          | linear vs **binary search**, lower-bound template |
| 14 | Sorting Algorithms                 | bubble/selection/insertion, **merge sort**, custom comparers |

### Part 5 — Core interview patterns
| #  | Topic | What you'll learn |
|----|-------|-------------------|
| 15 | Two Pointers & Sliding Window      | sorted Two Sum, longest-unique-substring, in-place edits |
| 16 | Hashing Patterns                   | frequency maps, Two Sum O(n), anagrams, group anagrams |
| 17 | Stack & Queue Patterns             | valid parentheses, monotonic stack, RPN, moving average |

### Part 6 — Data structures
| #  | Topic | What you'll learn |
|----|-------|-------------------|
| 18 | Linked Lists                       | build/traverse, **reverse**, cycle detection, find middle |
| 19 | Trees & Binary Search Trees        | insert, in/pre/post-order, **BFS level-order**, height |
| 20 | Graphs (BFS & DFS)                 | adjacency lists, shortest path, **number of islands** |
| 21 | Heaps & Priority Queues            | min/max-heap, **k-th largest**, top-K, merge k lists |

### Part 7 — Advanced algorithms
| #  | Topic | What you'll learn |
|----|-------|-------------------|
| 22 | Dynamic Programming                | climbing stairs, coin change, LCS, 0/1 knapsack |
| 23 | Backtracking                       | subsets, permutations, combinations, phone letters |

---

## 🗂️ Project structure

```
DSA-In-C#-The Beginning/
├── DsaInCSharp.sln              ← solution file
├── README.md
├── .gitignore
└── src/
    └── DsaInCSharp/
        ├── DsaInCSharp.csproj   ← project settings (target framework, etc.)
        ├── Program.cs           ← entry point (just launches the menu)
        ├── Core/
        │   ├── ILesson.cs       ← the contract every lesson implements
        │   └── LessonMenu.cs    ← the engine that finds & runs lessons
        └── Lessons/
            ├── Module01_InputOutput.cs
            ├── Module02_DataTypes.cs
            ├── ...
            └── Module23_Backtracking.cs
```

**The big idea:** the menu uses reflection to discover lessons automatically.
That means adding a topic is as simple as creating one new file — no wiring, no edits to existing code.

---

## ➕ Add your own lesson (module-by-module)

1. Create a new file in `src/DsaInCSharp/Lessons/`, e.g. `Module24_Tries.cs`.
2. Implement the `ILesson` interface:

```csharp
using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

public sealed class Module24_Tries : ILesson
{
    public int Order => 24;            // controls menu position (keep it unique)
    public string Title => "Tries";    // shown in the menu

    public void Run()
    {
        // Your demonstration + teaching comments go here.
        Console.WriteLine("Welcome to tries!");
    }
}
```

3. Run the app again — your lesson appears in the menu automatically. That's it. 🎉

### Ideas for future modules
Tries · Union-Find (Disjoint Set) · Greedy algorithms · Intervals · Prefix sums ·
Matrix traversal · Topological sort · Dijkstra/weighted graphs · Segment trees.

---

## 💡 How to study for interviews with this repo

1. **Read** a lesson's code top to bottom — the comments explain the *why*.
2. **Run** it and tweak the inputs; predict the output before you press Enter.
3. **Re-implement** the key method yourself in a scratch file without looking.
4. **Practice** matching LeetCode problems (the comments name many of the classics).
5. **Explain** the time/space complexity out loud — that's what interviewers want.

---

## 🤝 Contributing

Students are welcome to add lessons, fix typos, or improve explanations. Keep the spirit of the
project: **clear, well-commented, beginner-first code**. Open a pull request and help the next learner.

### Contribution checklist
- One concept per module; keep methods small and commented.
- Use a unique `Order` and a descriptive `Title`.
- Make sure `dotnet build` passes and the lesson runs from the menu.

## 📄 License

Free to use for learning. Add a license of your choice (e.g. MIT) before publishing widely.
