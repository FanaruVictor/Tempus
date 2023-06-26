
namespace FizzBuzz.Tests
{
    using FizzBuz;

    public class FizzBuzzTests
    {
        [Theory]
        [InlineData("15", "fizzbuzz")]
        [InlineData("10", "buzz")]
        [InlineData("3", "fizz")]
        [InlineData("7", "7")]
        [InlineData("Nan", "Not a number")]
        public void Given_String_When_GetFizzBuzzString_Then_ReturnExpectedValue(string value, string expected)
        {

            //Act 
            var actual = FizzBuzz.GetFizzBuzzString(value);

            //Assert
            Assert.Equal(expected, actual);
        }


    }
}