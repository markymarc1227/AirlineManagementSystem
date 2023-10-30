using AirlineManagementSystem.Validation;

namespace AirlineManagement.Tests.xUnit.FlightMaintenanceTests
{
    public class AirlineCodesShould
    {

        [Fact]
        public void InvalidateBlankAirlineCodeInput()
        {
            //Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            //Act
            InputValidation.ValidateAirlineCode("");

            //Assert
            var sb = writer.GetStringBuilder();
            Assert.Equal("Blank Airline code is not allowed.", sb.ToString().Trim());
        }

        [Theory]
        [InlineData("A")]
        [InlineData("AWS2")]
        [InlineData("PRAE2")]
        [InlineData("QWERTY")]
        [InlineData("QWERTY3S")]
        public void LimitCharacters(string airlineCode)
        {
            //Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            //Act
            InputValidation.ValidateAirlineCode(airlineCode);

            //Assert
            var sb = writer.GetStringBuilder();
            Assert.Equal("Airline Code must be 2 - 3 characters!", sb.ToString().Trim());
        }

        [Theory]
        [InlineData("PR")]
        [InlineData("5J")]
        [InlineData("TAM")]
        [InlineData("A4C")]
        [InlineData("BC3")]
        public void BeAlphaNumeric(string airlineCode)
        {
            bool isValid = InputValidation.ValidateAirlineCode(airlineCode);
            Assert.True(isValid);
        }
    }
}