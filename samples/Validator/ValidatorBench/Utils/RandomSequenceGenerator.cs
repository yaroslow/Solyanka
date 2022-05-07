using System;
using System.Text;

namespace ValidatorBench.Utils;

/// <summary>
/// Genetarot of random squences
/// </summary>
public class RandomSequenceGenerator
{
    private static readonly Random Random;

    static RandomSequenceGenerator()
    {
        Random = new Random();
    }

    
    /// <summary>
    /// Generate random string sequence
    /// </summary>
    /// <returns>Random string sequence</returns>
    public static string GenerateRandomString()
    {
        var length = Random.Next(0, 120);
        var stringBuilder = new StringBuilder(length);
        for (var i = 0; i < length; ++i)
        {
            var symbol = Random.Next(48, 123);
            stringBuilder.Append((char) symbol);
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Generate random int
    /// </summary>
    /// <returns>Random int</returns>
    public static int GenerateInt()
    {
        return Random.Next(10, 60);
    }
}