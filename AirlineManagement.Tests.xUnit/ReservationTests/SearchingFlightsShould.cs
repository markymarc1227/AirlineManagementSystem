using AirlineManagementSystem.Models;
using AirlineManagementSystem.Reservation;
using AirlineManagementSystem;
using AirlineManagementSystem.FlightMaintenance;

namespace AirlineManagement.Tests.xUnit.ReservationTests
{
    public class SearchingFlightsShould
    {
        [Fact]
        public void DisplayMatchesByAirlineCode()
        {
            //Arrange
            Initializers.InitializeFlightsAndBookings();
            //Act
            var availableReservations = FlightMaintenanceBusinessLogic.RetrieveResultsForAirlineCode("ABC");

            //Assert
            Assert.True(availableReservations.Any());
        }

        [Fact]
        public void DisplayNoMatchesByAirlineCode()
        {
            FlightMaintenanceBusinessLogic.flightsList.Clear();
            //Act
            var availableReservations = FlightMaintenanceBusinessLogic.RetrieveResultsForAirlineCode("ABC");
            //Assert
            Assert.False(availableReservations.Any());
        }

        [Fact]
        public void DisplayMatchesByFlightNumber()
        {
            //Arrange
            Initializers.InitializeFlightsAndBookings();
            //Act
            var availableReservations = FlightMaintenanceBusinessLogic.RetrieveResultsForFlightNumber(1234);

            //Assert
            Assert.True(availableReservations.Any());
        }

        [Fact]
        public void DisplayNoMatchesByFlightNumber()
        {
            FlightMaintenanceBusinessLogic.flightsList.Clear();
            //Act
            var availableReservations = FlightMaintenanceBusinessLogic.RetrieveResultsForFlightNumber(1234);
            //Assert
            Assert.False(availableReservations.Any());
        }

        [Fact]
        public void DisplayMatchesByOriginAndDestination()
        {
            //Arrange
            Initializers.InitializeFlightsAndBookings();
            //Act
            var availableReservations = FlightMaintenanceBusinessLogic.RetrieveResultsForOriginAndDestination("MNL","CEB");

            //Assert
            Assert.True(availableReservations.Any());
        }

        [Fact]
        public void DisplayNoMatchesByOriginAndDestination()
        {
            FlightMaintenanceBusinessLogic.flightsList.Clear();
            //Act
            var availableReservations = FlightMaintenanceBusinessLogic.RetrieveResultsForOriginAndDestination("MNL", "CEB");
            //Assert
            Assert.False(availableReservations.Any());
        }
    }
}
