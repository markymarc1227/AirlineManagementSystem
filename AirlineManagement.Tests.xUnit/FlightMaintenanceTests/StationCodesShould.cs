using AirlineManagementSystem.Validation;

namespace AirlineManagement.Tests.xUnit.FlightMaintenanceTests
{
    public class StationCodesShould
    {
        [Theory]
        [InlineData("A")]
        [InlineData("A2")]
        [InlineData("PRAE")]
        [InlineData("QWERTY")]
        [InlineData("MJFGS3")]
        public void Have3Characters(string stationCode)
        {
            //Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            //Act
            InputValidation.ValidateStation(stationCode);

            //Assert
            var sb = writer.GetStringBuilder();
            Assert.Equal("Station must be exactly 3 characters.", sb.ToString().Trim());
        }

        [Fact]
        public void InvalidateBlankFlightStationInput()
        {
            //Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            //Act
            InputValidation.ValidateStation("");

            //Assert
            var sb = writer.GetStringBuilder();
            Assert.Equal("Blank station code is not allowed.", sb.ToString().Trim());
        }

        [Theory]
        [InlineData("121")]
        [InlineData("2FD")]
        [InlineData("32H")]
        [InlineData("4W2")]
        [InlineData("509")]
        public void StartWithLetter(string stationCode)
        {
            //Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            //Act
            InputValidation.ValidateStation(stationCode);

            //Assert
            var sb = writer.GetStringBuilder();
            Assert.Equal("First Character must be a letter.", sb.ToString().Trim());
        }

        [Theory]
        [InlineData("A21")]
        [InlineData("BFD")]
        [InlineData("C2H")]
        [InlineData("DW2")]
        [InlineData("E09")]
        public void BeAlphaNumeric(string stationCode)
        {
            bool isValid = InputValidation.ValidateStation(stationCode);
            Assert.True(isValid);
        }


    }
}
