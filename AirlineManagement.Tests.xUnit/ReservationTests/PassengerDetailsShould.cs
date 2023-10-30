using AirlineManagementSystem.Reservation;
using AirlineManagementSystem.Validation;

namespace AirlineManagement.Tests.xUnit.ReservationTests
{
    public class PassengerDetailsShould
    {

        [Fact]
        public void CheckBlankFirstLastName()
        {
            //Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            //Act
            InputValidation.ValidatePassengerName("");

            //Assert
            var sb = writer.GetStringBuilder();
            Assert.Equal("Passenger Name is required!", sb.ToString().Trim());
        }

        [Fact]
        public void CheckNameMaxCharacter()
        {
            //Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            //Act
            InputValidation.ValidatePassengerName("somepersonsomepersonsome");

            //Assert
            var sb = writer.GetStringBuilder();
            Assert.Equal("Cannot Exceed 20 Characters!", sb.ToString().Trim());
        }

        [Fact]
        public void CheckPastDatedBirthDate()
        {
            var isPastDated = InputValidation.CheckIfPastDated(new DateTime(2023, 10, 25));
            Assert.True(isPastDated);
        }
        
        [Fact]
        public void ComputeCorrectAge()
        {
            int computedAge = FlightReservationBusinessLogic.CalculateBirthDay(new DateTime(2000, 10, 26));
            Assert.Equal(23,computedAge);
        }
        
        [Fact]
        public void CheckMaxPassengers()
        {
            //Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            //Act
            InputValidation.ValidatePassengerNumber("6");

            //Assert
            var sb = writer.GetStringBuilder();
            Assert.Equal("Passenger number has a max limit of 5.", sb.ToString().Trim());
            
        }



    }
}
