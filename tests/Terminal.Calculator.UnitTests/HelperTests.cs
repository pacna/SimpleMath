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
    }
}