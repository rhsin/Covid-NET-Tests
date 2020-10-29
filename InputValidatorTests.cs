using Covid.Services;
using System;
using Xunit;

namespace CovidTestProject
{
    public class InputValidatorTests
    {
        private InputValidator _inputValidator; 

        public InputValidatorTests()
        {
            _inputValidator = new InputValidator();
        }

        [Theory]
        [InlineData("nevada", true)]
        [InlineData("nEw YoRk", true)]
        [InlineData("maine1", false)]
        [InlineData("new, jersey", false)]
        [InlineData("montgomeryalabamamontgomeryalabama", false)]
        [InlineData("", true)]
        public void TestValidCounty(string county, bool expected)
        {
            Assert.Equal(expected, _inputValidator.IsValidCounty(county));
        }

        [Theory]
        [InlineData("nevada", true)]
        [InlineData("nEw YoRk", true)]
        [InlineData("maine1", false)]
        [InlineData("new, jersey", false)]
        [InlineData("montgomeryalabama", false)]
        [InlineData("", true)]
        public void TestValidState(string state, bool expected)
        {
            Assert.Equal(expected, _inputValidator.IsValidState(state));
        }

        [Theory]
        [InlineData("date", true)]
        [InlineData("count", false)]
        [InlineData("", false)]
        public void TestValidColumn(string column, bool expected)
        {
            Assert.Equal(expected, _inputValidator.IsValidColumn(column));
        }

        [Theory]
        [InlineData("desc", true)]
        [InlineData("ascc", false)]
        [InlineData("", true)]
        public void TestValidOrder(string order, bool expected)
        {
            Assert.Equal(expected, _inputValidator.IsValidOrder(order));
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(13, false)]
        [InlineData(0, false)]
        public void TestValidMonth(int month, bool expected)
        {
            Assert.Equal(expected, _inputValidator.IsValidMonth(month));
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(1200, false)]
        [InlineData(0, false)]
        public void TestValidLimit(int limit, bool expected)
        {
            Assert.Equal(expected, _inputValidator.IsValidLimit(limit));
        }
    }
}



