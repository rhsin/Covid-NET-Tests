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
        [InlineData("San Diego")]
        [InlineData("nEw YoRk")]
        [InlineData("montgomery")]
        public void TestValidCounty(string county)
        {
            Assert.True(_inputValidator.IsValidCounty(county));
        }

        [Theory]
        [InlineData("San Diego1")]
        [InlineData("nEw , YoRk")]
        [InlineData("montgomeryalabamamontgomeryalabama")]
        public void TestInvalidCounty(string county)
        {
            Assert.False(_inputValidator.IsValidCounty(county));
        }

        [Theory]
        [InlineData("New York")]
        [InlineData("nEw YoRk")]
        [InlineData("alabama")]
        public void TestValidState(string state)
        {
            Assert.True(_inputValidator.IsValidState(state));
        }

        [Theory]
        [InlineData("New York1")]
        [InlineData("nEw , YoRk")]
        [InlineData("montgomeryalabama")]
        public void TestInvalidState(string state)
        {
            Assert.False(_inputValidator.IsValidState(state));
        }
    }
}



