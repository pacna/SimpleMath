using Xunit;

namespace Terminal.Calculator.UnitTests
{
    public class HelperTests
    {
        [Theory]
        [InlineData("      3    +      3", "3+3")]
        [InlineData("  4 ^ 2 / 3 - 1    +      3", "4^2/3-1+3")]
        [InlineData("I like chocolate", "Ilikechocolate")]
        public void ShouldRemoveAllWhiteSpaces(string input, string expectedOutput)
        {
            // ACT
            string output = Helper.RemoveWhiteSpace(input);

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
            bool output = Helper.HasLetters(input);

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
            bool output = Helper.HasNumbers(input);

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
            bool output = Helper.HasInvalidSpecialCharacters(input);

            // ASSERT
            Assert.Equal(expectedOutput, output);
        }
    }
}