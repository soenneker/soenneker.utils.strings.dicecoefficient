using System;
using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Utils.Strings.DiceCoefficient;

/// <summary>
/// A utility library for comparing strings via the Dice Coefficient algorithm.
/// </summary>
public static class DiceCoefficientStringUtil
{
    /// <summary>
    /// Calculates the similarity percentage between two strings via Dice Coefficient.
    /// </summary>
    /// <param name="s1">The first string.</param>
    /// <param name="s2">The second string.</param>
    /// <returns>The similarity percentage between the two strings.</returns>
    public static double CalculatePercentage(string s1, string s2)
    {
        double similarity = Calculate(s1, s2);
        return similarity * 100;
    }

    /// <summary>
    /// Executes the calculate operation.
    /// </summary>
    /// <param name="s1">The s1.</param>
    /// <param name="s2">The s2.</param>
    /// <returns>The result of the operation.</returns>
    public static double Calculate(string s1, string s2)
    {
        bool isS1Empty = s1.IsNullOrEmpty();
        bool isS2Empty = s2.IsNullOrEmpty();

        if (isS1Empty || isS2Empty)
            return isS1Empty && isS2Empty ? 1.0 : 0.0;

        if (s1.Length < 2 || s2.Length < 2)
            return s1 == s2 ? 1.0 : 0.0;

        string indexed = s1.Length <= s2.Length ? s1 : s2;
        string scanned = ReferenceEquals(indexed, s1) ? s2 : s1;
        Dictionary<uint, int> frequencies = GetBigramsWithFrequency(indexed);
        var intersectionSize = 0;

        for (var i = 0; i < scanned.Length - 1; i++)
        {
            uint bigram = ((uint)scanned[i] << 16) | scanned[i + 1];
            if (!frequencies.TryGetValue(bigram, out int frequency) || frequency == 0)
                continue;

            intersectionSize++;
            frequencies[bigram] = frequency - 1;
        }

        // Calculate the Dice Coefficient
        double diceCoefficient = (2.0 * intersectionSize) / (s1.Length + s2.Length - 2);

        return diceCoefficient;
    }

    private static Dictionary<uint, int> GetBigramsWithFrequency(string input)
    {
        var bigrams = new Dictionary<uint, int>(input.Length - 1);

        for (var i = 0; i < input.Length - 1; i++)
        {
            uint bigram = ((uint)input[i] << 16) | input[i + 1];

            if (!bigrams.TryAdd(bigram, 1))
            {
                bigrams[bigram]++;
            }
        }

        return bigrams;
    }
}
