using AwesomeAssertions;
using Soenneker.Tests.HostedUnit;


namespace Soenneker.Utils.Strings.DiceCoefficient.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class DiceCoefficientStringUtilTests : HostedUnitTest
{
    public DiceCoefficientStringUtilTests(Host host) : base(host)
    {
    }

    [Test]
    [Arguments("", "", 1)] // Two empty strings are identical
    [Arguments("abc", "", 0)] // One empty string results in no similarity
    [Arguments("", "xyz", 0)] // One empty string results in no similarity
    [Arguments("kitten", "sitting", 0.3636)] // Calculated Dice Coefficient
    [Arguments("kitten", "kitten", 1)] // Identical strings have full similarity
    [Arguments("abc", "def", 0)] // Completely different strings, no shared bigrams
    [Arguments("abcdef", "abc", 0.5714)] // Calculated Dice Coefficient
    [Arguments("abc", "abcd", 0.8)] // Calculated Dice Coefficient
    [Arguments("this is sitting on a porch", "this is sitting", 0.717)] // Calculated similarity
    [Arguments("the cat sat on a hat", "sad on a hat", 0.6)] // Calculated similarity
    [Arguments("this is a test", "this is another test", 0.75)] // Calculated similarity

    public void Calculate_Returns_Correct_Similarity_Score(string str1, string str2, double expectedScore)
    {
        double similarityScore = DiceCoefficientStringUtil.Calculate(str1, str2);

        similarityScore.Should().BeApproximately(expectedScore, 0.001);
    }
}
