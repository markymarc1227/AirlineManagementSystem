using AirlineManagementSystem;
using AirlineManagementSystem.Models;
using AirlineManagementSystem.Reservation;
using AirlineManagementSystem.Validation;

namespace AirlineManagement.Tests.xUnit.ReservationTests
{
    public class SearchingListingReservationsShould
    {

        [Fact]
        public void DisplayAllExistingReservations()
        {
            //Arrange
            Initializers.InitializeFlightsAndBookings();
            //Act
            var availableReservations = FlightReservationBusinessLogic.SearchAvailableReservations();

            //Assert
            Assert.True(availableReservations.Any());
        }

        [Fact]
        public void DisplayNoExistingReservations()
        {
            FlightReservationBusinessLogic.bookingsList.Clear();
            //Act
            List<Booking> availableReservations = FlightReservationBusinessLogic.SearchAvailableReservations();
            //Assert
            Assert.False(availableReservations.Any());
        }
    }
}
