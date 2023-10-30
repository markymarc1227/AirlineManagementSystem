using AirlineManagementSystem;
using AirlineManagementSystem.FlightMaintenance;
using AirlineManagementSystem.Validation;

namespace AirlineManagement.Tests.xUnit.FlightMaintenanceTests
{
    public class DuplicateFlightsShould
    {
        [Theory]
        [InlineData("ABC",1234,"CEB","MNL")]
        [InlineData("ABC",1235,"SIN","MNL")]
        public void BeCheckedIfExists(string airlineCode, int flightNumber, string arrivalStation, string departureStation)
        {
            Initializers.InitializeFlightsAndBookings();
            bool isValid = FlightMaintenanceBusinessLogic.CheckForDuplicateFlights(airlineCode, flightNumber, arrivalStation, departureStation);
            Assert.True(isValid);
        }
        
    }
}
