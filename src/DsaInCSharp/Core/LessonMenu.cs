using System.Reflection;

namespace DsaInCSharp.Core;

/// <summary>
/// The interactive menu that powers the whole app.
///
/// It uses a touch of "reflection" to automatically find every class in this project
/// that implements <see cref="ILesson"/>. The big win for learners:
///
///     To add a new topic you ONLY create a new lesson class.
///     You never have to edit this file or register anything by hand.
///
/// Don't worry if reflection feels advanced right now — you can treat this class as the
/// "engine" and focus on writing lessons. Come back and read it once you're comfortable.
/// </summary>
public sealed class LessonMenu
{
    private readonly List<ILesson> _lessons;

    public LessonMenu()
    {
        // 1. Look inside THIS assembly (our compiled project).
        // 2. Find every concrete (non-abstract) class that implements ILesson.
        // 3. Create one instance of each and sort them by their Order property.
        _lessons = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => typeof(ILesson).IsAssignableFrom(type)
                           && type is { IsInterface: false, IsAbstract: false })
            .Select(type => (ILesson)Activator.CreateInstance(type)!)
            .OrderBy(lesson => lesson.Order)
            .ToList();
    }

    /// <summary>
    /// Shows the menu in a loop until the user chooses to quit.
    /// </summary>
    public void Show()
    {
        while (true)
        {
            SafeClear();
            PrintHeader();
            PrintLessonList();

            Console.Write("\nPick a lesson number (or 'q' to quit): ");
            string? input = Console.ReadLine()?.Trim();

            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\nHappy learning! See you in the next session. 👋");
                return;
            }

            // int.TryParse safely converts text to a number WITHOUT crashing on bad input.
            // This pattern (TryParse) shows up constantly in interviews and real code.
            if (int.TryParse(input, out int choice))
            {
                ILesson? selected = _lessons.FirstOrDefault(lesson => lesson.Order == choice);
                if (selected is not null)
                {
                    RunLesson(selected);
                    continue;
                }
            }

            Console.WriteLine("\n⚠️  That wasn't a valid choice. Press any key to try again...");
            SafeReadKey();
        }
    }

    private void RunLesson(ILesson lesson)
    {
        SafeClear();
        Console.WriteLine($"=== Lesson {lesson.Order}: {lesson.Title} ===\n");

        lesson.Run();

        Console.WriteLine("\n-------------------------------------------------");
        Console.WriteLine("Lesson finished. Press any key to return to the menu...");
        SafeReadKey();
    }

    // Console.Clear() throws if the program runs without a real interactive console
    // (for example when input is piped in). This wrapper keeps the app from crashing there.
    private static void SafeClear()
    {
        try
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }
        }
        catch (IOException)
        {
            // No console to clear — safe to ignore and just continue.
        }
    }

    // Waits for a key press, but only when a real console is attached.
    private static void SafeReadKey()
    {
        if (!Console.IsInputRedirected)
        {
            Console.ReadKey();
        }
    }

    private static void PrintHeader()
    {
        Console.WriteLine("=================================================");
        Console.WriteLine("   DSA in C# — The Beginning");
        Console.WriteLine("   A structured, beginner-friendly study journey");
        Console.WriteLine("=================================================\n");
    }

    private void PrintLessonList()
    {
        if (_lessons.Count == 0)
        {
            Console.WriteLine("No lessons found yet. Add a class that implements ILesson to get started!");
            return;
        }

        Console.WriteLine("Available lessons:\n");
        foreach (ILesson lesson in _lessons)
        {
            Console.WriteLine($"  {lesson.Order,2}.  {lesson.Title}");
        }
    }
}
