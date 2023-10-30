using AirlineManagementSystem.Reservation;
using AirlineManagementSystem;

namespace AirlineManagement.Tests.xUnit.ReservationTests
{
    public class SearchingReservationsByPnrShould
    {

        [Fact]
        public void DisplayReservationByPNR()
        {
            //Arrange
            Initializers.InitializeFlightsAndBookings();
            //Act
            var availableReservation = FlightReservationBusinessLogic.retrieveReservationByPNR("XYZ3MD");
            //Assert
            Assert.True(availableReservation.Any());
        }

        [Fact]
        public void DisplayUnavailableReservationByPNR()
        {
            //Arrange
            FlightReservationBusinessLogic.bookingsList.Clear();
            //Act
            var availableReservation = FlightReservationBusinessLogic.retrieveReservationByPNR("XYZ3MD");
            //Assert
            Assert.False(availableReservation.Any());
        }
    }
}
