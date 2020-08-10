using System;
using Xunit;
using System.Collections.Generic;
using Terminal.Calculator;

namespace Terminal.Calculator.UnitTests
{
    public class ShuntingYardTests
    {
        [Theory]
        [InlineData("3+3", "33+")]
        [InlineData("3 ^ 4 ^ 2", "342^^")]
        [InlineData(" 4 +4 * 2 /(1 -     5)", "442*15-/+")]
        [InlineData("3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3", "342*15-23^^/+")]
        public void ShouldDisplayTheCorrectPostfixNotation(string input, string expectedOutput)
        {
            // ARRANGE
            ShuntingYard sy = new ShuntingYard(input);
            sy.ConvertToPostfix();

            // ACT
            string output = sy.GetPostfix();

            // ASSERT
            Assert.Equal(expectedOutput, output);
        }

        [Theory]
        [InlineData("7 - 4", 3.0)]
        [InlineData("3 - 6", -3.0)]
        [InlineData("6/3*4", 8.0)]
        [InlineData("3^4^2", 43046721.0)]
        [InlineData("7 + (6 * 5^2 + 3)", 160.0)]
        public void ShouldEvaluatetheExpressionCorrectly(string input, double expectedOutput)
        {
            // ARRANGE
            ShuntingYard sy = new ShuntingYard(input);
            sy.ConvertToPostfix();

            // ACT
            double output = sy.Evaluate();

            // ASSERT
            Assert.Equal(expectedOutput, output);
        }
    }
}
