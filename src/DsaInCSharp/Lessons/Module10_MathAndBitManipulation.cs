using DsaInCSharp.Core;

namespace DsaInCSharp.Lessons;

/// <summary>
/// LESSON 10 — Math &amp; Bit Manipulation
///
/// A toolbox of numeric tricks that unlock many "clever" LeetCode solutions.
/// Goal:
///   - integer vs floating division, modulo, overflow (and how to avoid it)
///   - useful Math methods
///   - binary basics and the core bit operators (&amp; | ^ ~ &lt;&lt; &gt;&gt;)
///   - classic bit tricks: even/odd, power-of-two, count set bits, swap, single number
/// </summary>
public sealed class Module10_MathAndBitManipulation : ILesson
{
    public int Order => 10;
    public string Title => "Math & Bit Manipulation";

    public void Run()
    {
        // --- DIVISION & MODULO ---------------------------------------
        Console.WriteLine($"7 / 2   = {7 / 2}   (integer division truncates)");
        Console.WriteLine($"7 / 2.0 = {7 / 2.0} (floating division)");
        Console.WriteLine($"7 % 3   = {7 % 3}   (remainder — used constantly)");

        // Avoiding overflow when finding a midpoint (a famous binary-search bug):
        int low = 2_000_000_000, high = 2_000_000_001;
        int badMid = (low + high) / 2;          // overflows int! becomes negative
        int safeMid = low + (high - low) / 2;   // correct, no overflow
        Console.WriteLine($"Overflow mid = {badMid}, safe mid = {safeMid}");

        Console.WriteLine();

        // --- USEFUL Math METHODS -------------------------------------
        Console.WriteLine($"Abs(-9) = {Math.Abs(-9)}");
        Console.WriteLine($"Max(3,8) = {Math.Max(3, 8)}, Min(3,8) = {Math.Min(3, 8)}");
        Console.WriteLine($"Pow(2,10) = {Math.Pow(2, 10)}");
        Console.WriteLine($"Sqrt(144) = {Math.Sqrt(144)}");
        Console.WriteLine($"GCD(48,36) = {Gcd(48, 36)}");

        Console.WriteLine();

        // --- BINARY & BITWISE ----------------------------------------
        int a = 0b1100; // 12
        int b = 0b1010; // 10
        Console.WriteLine($"a = {a} = {ToBin(a)}");
        Console.WriteLine($"b = {b} = {ToBin(b)}");
        Console.WriteLine($"a & b (AND) = {ToBin(a & b)}  (bits set in BOTH)");
        Console.WriteLine($"a | b (OR ) = {ToBin(a | b)}  (bits set in EITHER)");
        Console.WriteLine($"a ^ b (XOR) = {ToBin(a ^ b)}  (bits that DIFFER)");
        Console.WriteLine($"a << 1      = {ToBin(a << 1)}  (left shift = multiply by 2)");
        Console.WriteLine($"a >> 1      = {ToBin(a >> 1)}  (right shift = divide by 2)");

        Console.WriteLine();

        // --- CLASSIC BIT TRICKS --------------------------------------
        int x = 13;
        Console.WriteLine($"{x} is odd? (x & 1) == 1 → {(x & 1) == 1}");
        Console.WriteLine($"IsPowerOfTwo(16) = {IsPowerOfTwo(16)}, IsPowerOfTwo(18) = {IsPowerOfTwo(18)}");
        Console.WriteLine($"CountSetBits(13) = {CountSetBits(13)}  (13 = 1101 → three 1s)");

        // XOR magic: every number XORed with itself is 0, so duplicates cancel out.
        // This finds the ONE number that appears an odd number of times — O(n) time, O(1) space.
        int[] nums = { 4, 1, 2, 1, 2 };
        int single = 0;
        foreach (int n in nums) single ^= n;
        Console.WriteLine($"Single number in [4,1,2,1,2] = {single}");

        Console.WriteLine();
        Console.WriteLine("Takeaway: modulo, overflow-safe midpoints, and XOR/shift tricks");
        Console.WriteLine("show up again and again — keep them in your back pocket.");
    }

    // Euclid's algorithm — greatest common divisor.
    private static int Gcd(int a, int b) => b == 0 ? a : Gcd(b, a % b);

    // A power of two has exactly one bit set; n & (n-1) clears the lowest set bit.
    private static bool IsPowerOfTwo(int n) => n > 0 && (n & (n - 1)) == 0;

    private static int CountSetBits(int n)
    {
        int count = 0;
        while (n != 0)
        {
            n &= (n - 1);   // repeatedly remove the lowest set bit
            count++;
        }
        return count;
    }

    private static string ToBin(int n) => Convert.ToString(n, 2).PadLeft(5, '0');
}
