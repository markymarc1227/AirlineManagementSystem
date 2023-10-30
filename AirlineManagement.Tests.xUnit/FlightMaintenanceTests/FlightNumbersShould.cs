using AirlineManagementSystem.Validation;

namespace AirlineManagement.Tests.xUnit.FlightMaintenanceTests
{
    public class FlightNumbersShould
    {
        [Theory]
        [InlineData("1")]
        [InlineData("20")]
        [InlineData("300")]
        [InlineData("4000")]
        [InlineData("9999")]
        public void AllowRange1to9999(string flightNumber)
        {
            int convertedFlightNumber = InputValidation.ValidateFlightNumber(flightNumber);
            Assert.Equal(int.Parse(flightNumber), convertedFlightNumber);
        }

        [Fact]
        public void InvalidateBlankFlightNumberInput()
        {
            //Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            //Act
            InputValidation.ValidateFlightNumber("");

            //Assert
            var sb = writer.GetStringBuilder();
            Assert.Equal("Blank Flight Number is not allowed.", sb.ToString().Trim());
        }

        [Theory]
        [InlineData("!(*A")]
        [InlineData("!@!$@")]
        [InlineData("@!32")]
        [InlineData("QWTY1")]
        [InlineData("!@(&@")]
        public void PreventInvalidCharacters(string flightNumber)
        {
            //Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            //Act
            InputValidation.ValidateFlightNumber(flightNumber);

            //Assert
            var sb = writer.GetStringBuilder();
            Assert.Equal("Please enter a number.", sb.ToString().Trim());
        }
    }
}
