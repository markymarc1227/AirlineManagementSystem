using AirlineManagementSystem.Validation;
using System.Globalization;

namespace AirlineManagement.Tests.xUnit.FlightMaintenanceTests
{
    public class StdAndStaShould
    {
        [Fact]
        public void ValidateCorrectTimeFormat()
        {
            DateTime confirmedParsedTime;
            InputValidation.ParseTime("23:43", out confirmedParsedTime);

            DateTime expectedOutput = DateTime.ParseExact("23:43", "HH:mm", CultureInfo.InvariantCulture);

            Assert.Equal(expectedOutput, confirmedParsedTime);
        }

        [Fact]
        public void InvalidateWrongTimeFormat()
        {
            DateTime confirmedParsedTime;
            bool isValidTime = InputValidation.ParseTime("43:34:12", out confirmedParsedTime);
            Assert.False(isValidTime);
        }
    }
}
