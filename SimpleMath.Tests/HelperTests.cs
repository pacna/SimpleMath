using SimpleMath.PostfixMath;

namespace SimpleMath.Tests;

public class HelperTests
{
    [Theory]
    [InlineData("      3    +      3", "3+3")]
    [InlineData("  4 ^ 2 / 3 - 1    +      3", "4^2/3-1+3")]
    [InlineData("I like chocolate", "Ilikechocolate")]
    public void ShouldRemoveAllWhiteSpaces(string input, string expectedOutput)
    {
        // ACT
        string output = Helper.RemoveWhiteSpace(input: input);

        // ASSERT
        Assert.Equal(expectedOutput, output);
    }

    [Theory]
    [InlineData('a', false)]
    [InlineData('(', false)]
    [InlineData(')', false)]
    [InlineData('3', false)]
    [InlineData('+', true)]
    [InlineData('-', true)]
    [InlineData('*', true)]
    [InlineData('/', true)]
    [InlineData('^', true)]
    public void ShouldCheckIfFirstElementIsAnArithmeticOperator(char input, bool expectedOutput)
    {
        // ACT
        bool output = Helper.IsArithmeticOperator(input: input);

        // ASSERT
        Assert.Equal(expectedOutput, output);
    }

    [Theory]
    [InlineData("abc", false)]
    [InlineData("3", true)]
    [InlineData("3000", true)]
    [InlineData("99", true)]
    [InlineData("this is clearly not a number", false)]
    public void ShoulCheckIfItIsANumber(string input, bool expectedOutput)
    {
        // ACT
        bool output = Helper.IsNumber(input: input);

        // ASSERT
        Assert.Equal(expectedOutput, output);
    }

    [Theory]
    [InlineData("abc", true)]
    [InlineData("11112", false)]
    [InlineData("3x + y", true)]
    public void ShouldCheckIfThereIsAnyLetters(string input, bool expectedOutput)
    {
        // ACT
        bool output = Helper.HasLetters(input: input);

        // ASSERT
        Assert.Equal(expectedOutput, output);
    }

    [Theory]
    [InlineData("abc", false)]
    [InlineData("+-------", false)]
    [InlineData("3+3", true)]
    public void ShouldCheckIfThereIsAnyNumbers(string input, bool expectedOutput)
    {
        // ACT
        bool output = Helper.HasNumbers(input: input);

        // ASSERT
        Assert.Equal(expectedOutput, output);
    }

    [Theory]
    [InlineData("3+3=", true)]
    [InlineData("4^2/3-1+3", false)]
    [InlineData("4^$/3-1+3@@@", true)]
    public void ShouldCheckIfThereIsAnyInvalidSpecialCharacters(string input, bool expectedOutput)
    {
        // ACT
        bool output = Helper.HasInvalidSpecialCharacters(input: input);

        // ASSERT
        Assert.Equal(expectedOutput, output);
    }
}