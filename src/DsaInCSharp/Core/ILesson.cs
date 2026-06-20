namespace DsaInCSharp.Core;

/// <summary>
/// The contract that EVERY lesson module must implement.
///
/// Why an interface?
///   - It defines a common "shape" for all lessons so the menu can treat them uniformly.
///   - To add a brand-new topic, you simply create a class that implements this interface.
///     The menu discovers it automatically (see <see cref="LessonMenu"/>) — no other wiring needed.
///
/// This is also a tiny taste of two interview favourites:
///   - Polymorphism (many classes, one shared interface)
///   - The "Open/Closed Principle" (open to add new lessons, closed for modifying the runner).
/// </summary>
public interface ILesson
{
    /// <summary>
    /// A short number used to order and select the lesson in the menu, e.g. 1, 2, 3...
    /// Keep these unique. Lower numbers appear first.
    /// </summary>
    int Order { get; }

    /// <summary>
    /// The human-friendly title shown in the menu, e.g. "Input / Output Basics".
    /// </summary>
    string Title { get; }

    /// <summary>
    /// The actual teaching code for the topic. The menu calls this when the user picks the lesson.
    /// Put your demonstrations, comments, and Console.WriteLine explanations here.
    /// </summary>
    void Run();
}
