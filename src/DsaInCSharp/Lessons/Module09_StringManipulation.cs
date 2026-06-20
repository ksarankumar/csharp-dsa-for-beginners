using System.Text;
using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 9 — String Manipulation
///
/// Strings appear in a huge share of LeetCode problems. KEY interview fact:
/// strings in C# are IMMUTABLE — every "change" creates a new string. So when
/// building strings in a loop, use StringBuilder.
///
/// Goal:
///   - common string methods (Substring, IndexOf, Split, Replace, etc.)
///   - char tricks (char.IsDigit/IsLetter, char arithmetic)
///   - StringBuilder for efficient building
///   - reverse a string and check a palindrome (classic warm-ups)
/// </summary>
public sealed class Module09_StringManipulation : ILesson
{
    public int Order => 9;
    public string Title => "String Manipulation";

    public void Run()
    {
        string text = "Hello, DSA World";

        // --- INSPECTING ----------------------------------------------
        Console.WriteLine($"Length: {text.Length}");
        Console.WriteLine($"Upper:  {text.ToUpper()}");
        Console.WriteLine($"Index of 'DSA': {text.IndexOf("DSA")}");
        Console.WriteLine($"Contains 'World': {text.Contains("World")}");
        Console.WriteLine($"Substring(7,3): {text.Substring(7, 3)}");
        Console.WriteLine($"Replace: {text.Replace("World", "Champions")}");

        // --- SPLIT & JOIN --------------------------------------------
        string csv = "apple,banana,cherry";
        string[] parts = csv.Split(',');
        Console.WriteLine($"Split count: {parts.Length}, joined with ' | ': {string.Join(" | ", parts)}");

        // Trim removes leading/trailing whitespace — common when parsing input.
        Console.WriteLine($"Trimmed: '{"   spaced   ".Trim()}'");

        Console.WriteLine();

        // --- CHARACTERS ----------------------------------------------
        // A string is a sequence of chars; index it like an array.
        char first = text[0];
        Console.WriteLine($"First char: {first}, is letter? {char.IsLetter(first)}");

        // char arithmetic: letters map to numbers. 'a'=97. This converts a digit char to its int.
        char digit = '7';
        int digitValue = digit - '0';      // classic trick: '7' - '0' == 7
        Console.WriteLine($"Char '7' as int = {digitValue}");

        // Position of a lowercase letter in the alphabet (0-based).
        Console.WriteLine($"'c' - 'a' = {'c' - 'a'} (so 'c' is the 4th letter)");

        Console.WriteLine();

        // --- STRINGBUILDER (efficient building) ----------------------
        // BAD in a hot loop: result += ch  (creates a new string each time → O(n^2))
        // GOOD: StringBuilder mutates one buffer → O(n).
        var sb = new StringBuilder();
        for (char c = 'A'; c <= 'F'; c++)
        {
            sb.Append(c);
        }
        Console.WriteLine($"Built with StringBuilder: {sb}");

        Console.WriteLine();

        // --- CLASSIC WARM-UPS ----------------------------------------
        Console.WriteLine($"Reverse(\"algorithm\") = {Reverse("algorithm")}");
        Console.WriteLine($"IsPalindrome(\"racecar\") = {IsPalindrome("racecar")}");
        Console.WriteLine($"IsPalindrome(\"hello\")   = {IsPalindrome("hello")}");
    }

    private static string Reverse(string s)
    {
        char[] chars = s.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    // Two-pointer palindrome check: compare ends moving inward. O(n) time, O(1) extra space.
    private static bool IsPalindrome(string s)
    {
        int left = 0, right = s.Length - 1;
        while (left < right)
        {
            if (s[left] != s[right]) return false;
            left++;
            right--;
        }
        return true;
    }
}
