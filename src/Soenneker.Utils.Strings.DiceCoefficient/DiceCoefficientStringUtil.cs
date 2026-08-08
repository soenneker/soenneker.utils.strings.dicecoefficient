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

        // Generate bigrams and calculate intersection simultaneously
        Dictionary<uint, int> bigrams1 = GetBigramsWithFrequency(s1, out int totalBigrams1);
        Dictionary<uint, int> bigrams2 = GetBigramsWithFrequency(s2, out int totalBigrams2);
        int intersectionSize = CountIntersection(bigrams1, bigrams2);

        // Calculate the Dice Coefficient
        double diceCoefficient = (2.0 * intersectionSize) / (totalBigrams1 + totalBigrams2);

        return diceCoefficient;
    }

    private static Dictionary<uint, int> GetBigramsWithFrequency(string input, out int totalFrequency)
    {
        totalFrequency = Math.Max(0, input.Length - 1);
        var bigrams = new Dictionary<uint, int>(totalFrequency);

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

    private static int CountIntersection(Dictionary<uint, int> bigrams1, Dictionary<uint, int> bigrams2)
    {
        var intersectionCount = 0;

        foreach (KeyValuePair<uint, int> kvp in bigrams1)
        {
            if (bigrams2.TryGetValue(kvp.Key, out int frequencyInBigrams2))
            {
                intersectionCount += Math.Min(kvp.Value, frequencyInBigrams2);
            }
        }

        return intersectionCount;
    }
}
