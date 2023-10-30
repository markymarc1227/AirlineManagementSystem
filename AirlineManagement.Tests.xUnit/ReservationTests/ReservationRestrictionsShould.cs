using AirlineManagementSystem.FlightMaintenance;
using AirlineManagementSystem;
using AirlineManagementSystem.Validation;
using AirlineManagementSystem.Reservation;

namespace AirlineManagement.Tests.xUnit.ReservationTests
{
    public class ReservationRestrictionsShould
    {
        [Fact]
        public void RetrieveFlightDetails()
        {
            //Arrange
            Initializers.InitializeFlightsAndBookings();
            //Act
            var availableReservations = FlightMaintenanceBusinessLogic.RetrieveResultsForAirlineCode("ABC");
            //Assert
            Assert.True(availableReservations.Any());
        }
        [Fact]
        public void FutureDatedFlightDate()
        {
            var isFutureDated = InputValidation.CheckIfFutureDated(new DateTime(2023, 10, 30));
            Assert.True(isFutureDated);
        }

        [Theory]
        [InlineData("1###MD")]
        [InlineData("23@331")]
        [InlineData("3)Z3#D")]
        public void HavePNRStartWithLetter(string pnr)
        {
            //Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            //Act
            InputValidation.CheckPnrConditions(pnr);

            //Assert
            var sb = writer.GetStringBuilder();
            Assert.Equal("PNR Must start with a letter.", sb.ToString().Trim());
        }

        [Theory]
        [InlineData("A###MD")]
        [InlineData("B3@331")]
        [InlineData("C)Z3#D")]
        public void HaveAlphanumericPNR(string pnr)
        {   
            //Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            //Act
            InputValidation.CheckPnrConditions(pnr);

            //Assert
            var sb = writer.GetStringBuilder();
            Assert.Equal("Only Alphanumeric Characters are allowed.", sb.ToString().Trim());
        }

        [Fact]
        public void HaveUniquePNR()
        {
            //Arrange
            Initializers.InitializeFlightsAndBookings();
            //Act
            var availableReservations = FlightReservationBusinessLogic.SearchDuplicatePnr("ABZ3MD");
            //Assert
            Assert.Single(availableReservations);
        }
    }
}
