using AwesomeAssertions;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Utils.Strings.DiceCoefficient.Tests;

[Collection("Collection")]
public class DiceCoefficientStringUtilTests : FixturedUnitTest
{
    public DiceCoefficientStringUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Theory]
    [InlineData("", "", 1)] // Two empty strings are identical
    [InlineData("abc", "", 0)] // One empty string results in no similarity
    [InlineData("", "xyz", 0)] // One empty string results in no similarity
    [InlineData("kitten", "sitting", 0.3636)] // Calculated Dice Coefficient
    [InlineData("kitten", "kitten", 1)] // Identical strings have full similarity
    [InlineData("abc", "def", 0)] // Completely different strings, no shared bigrams
    [InlineData("abcdef", "abc", 0.5714)] // Calculated Dice Coefficient
    [InlineData("abc", "abcd", 0.8)] // Calculated Dice Coefficient
    [InlineData("this is sitting on a porch", "this is sitting", 0.717)] // Calculated similarity
    [InlineData("the cat sat on a hat", "sad on a hat", 0.6)] // Calculated similarity
    [InlineData("this is a test", "this is another test", 0.75)] // Calculated similarity

    public void Calculate_Returns_Correct_Similarity_Score(string str1, string str2, double expectedScore)
    {
        double similarityScore = DiceCoefficientStringUtil.Calculate(str1, str2);

        similarityScore.Should().BeApproximately(expectedScore, 0.001);
    }
}