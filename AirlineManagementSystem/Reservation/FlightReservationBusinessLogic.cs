using AirlineManagementSystem.Models;
using AirlineManagementSystem.FlightMaintenance;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineManagementSystem.Validation;

namespace AirlineManagementSystem.Reservation
{
    /// <summary>
    /// Contains all the methods and logic used in creating, showing, and filtering reservations.
    /// </summary>
    public class FlightReservationBusinessLogic
    {
        public static List<Booking> bookingsList = new();

        /// <summary>
        /// Starts the creation of a new reservation/booking by asking the user to enter an airline code and flight number.
        /// If a flight is available, it proceeds to ask for the input involving booking details.
        /// Creates a new reservation object if all input fields are complied.
        /// </summary>
        public static void CreateReservation()
        {
            string airlineCode;
            int flightNumber;
            DateTime flightDate;
            string pnrNumber;
            int numberOfPassengers;
            List<Passenger> passengers;

            airlineCode = InputValidation.GetAirlineCode("Enter Airline Code: ");
            flightNumber = InputValidation.GetFlightNumber("Enter Flight Number: ");

            List<Flight> flights = CheckFlightAvailability(airlineCode, flightNumber);
            if (flights.Any())
            {
                Console.WriteLine($"Flight/s Found!");

                int validatedFlightIndex = InputValidation.ValidateFlightIndex("Enter the index of the flight you want to book: ", flights);
                while (validatedFlightIndex == -1) 
                {
                    Console.WriteLine("Please select a valid flight index.");
                    validatedFlightIndex = InputValidation.ValidateFlightIndex("Enter the index of the flight you want to book: ", flights);
                }

                Flight selectedFlight = flights[validatedFlightIndex];
                Console.WriteLine($"********Please Enter Reservation Details for {selectedFlight.AirlineCode} {selectedFlight.FlightNumber} " +
                    $"{selectedFlight.DepartureStation}-{selectedFlight.ArrivalStation}********");

                flightDate = InputValidation.ValidateDateFormat("Enter Flight Date (MM/dd/yyyy): ");
                while (!(InputValidation.CheckIfFutureDated(flightDate))) 
                {
                    Console.WriteLine("Flight date must be future-dated.");
                    flightDate = InputValidation.ValidateDateFormat("Enter Flight Date (MM/dd/yyyy): ");
                }

                numberOfPassengers = InputValidation.GetPassengerNumber("Enter Number of Passengers (Max 5): ");
                passengers = InputPassengerDetails(numberOfPassengers);

                if (ConfirmReservation(selectedFlight, flightDate, numberOfPassengers, passengers))
                {
                    pnrNumber = GeneratePnrNumber();
                    while (SearchDuplicatePnr(pnrNumber).Any()) 
                    {
                        pnrNumber = GeneratePnrNumber();
                    }

                    Booking newBooking = new Booking(selectedFlight, flightDate, numberOfPassengers, passengers, pnrNumber);
                    bookingsList.Add(newBooking);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Booking has been confirmed. PNR Number is {pnrNumber}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nReservation has been cancelled.\n");
                };

            }
            else
            {
                Console.WriteLine("\nNO FLIGHTS AVAILABLE :(");
            };
        }

        /// <summary>
        /// Displays all the current reservations made.
        /// </summary>
        public static void ShowAllReservations()
        {
            List<Booking> searchResults = SearchAvailableReservations();

            if (searchResults.Any())
            {
                DisplayBookingSearchResults(searchResults);
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine("Reservations were successfully loaded.");
            }
            else { 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No Existing Reservations.");
            };
        }

        /// <summary>
        /// Method containing the LINQ statement to retrieve all bookings from the booking repository.
        /// </summary>
        /// <returns>List of all bookings made.</returns>
        public static List<Booking> SearchAvailableReservations() 
        {
            List<Booking> searchResults;
            searchResults = (from booking in bookingsList select booking).ToList();
            return searchResults;
        }

        /// <summary>
        /// Displays the reservations based on a given PNR.
        /// </summary>
        public static void SearchByPNR()
        {
            List<Booking> searchResult;
            string pnr = InputValidation.ValidatePnr("Enter a PNR Number: ");

            searchResult = retrieveReservationByPNR(pnr);

            DisplayBookingSearchResults(searchResult);
        }

        /// <summary>
        /// Gets a single booking based on a provided PNR.
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns>A booking that has the same PNR.</returns>
        public static List<Booking> retrieveReservationByPNR(string pnr) 
        {
            List<Booking> searchResult;
            searchResult = (from booking in bookingsList
                            where booking.PNR == pnr
                            select booking).ToList();
            return searchResult;
        }

        /// <summary>
        /// Displays the summary of the reservation and asks the user to confirm the booking.
        /// </summary>
        /// <param name="selectedFlight"></param>
        /// <param name="flightDate"></param>
        /// <param name="numberOfPassengers"></param>
        /// <param name="passengers"></param>
        /// <returns>A boolean based on Yes or No from the user.</returns>
        public static bool ConfirmReservation(Flight selectedFlight, DateTime flightDate,
            int numberOfPassengers, List<Passenger> passengers)
        {
            Console.WriteLine("\n*************Reservation Summary*************");
            Console.WriteLine($"----------------------------------");
            Console.WriteLine($"|Airline Code: {selectedFlight.AirlineCode}                |");
            Console.WriteLine($"----------------------------------");
            Console.WriteLine($"|Flight Number: {selectedFlight.FlightNumber}              |");
            Console.WriteLine($"----------------------------------");
            Console.WriteLine($"|Arrival Station: {selectedFlight.ArrivalStation}             |");
            Console.WriteLine($"----------------------------------");
            Console.WriteLine($"|Departure Station: {selectedFlight.DepartureStation}           |");
            Console.WriteLine($"----------------------------------");
            Console.WriteLine($"|Departure Time: {selectedFlight.STD.ToString("HH:mm")}            |");
            Console.WriteLine($"----------------------------------");
            Console.WriteLine($"|Arrival Time: {selectedFlight.STA.ToString("HH:mm")}              |");
            Console.WriteLine($"----------------------------------");
            Console.WriteLine($"|Flight Date: {flightDate.ToString("MM/dd/yyyy")}          |");
            Console.WriteLine($"----------------------------------");
            Console.WriteLine($"|Number of Passengers: {passengers.Count()}          |");
            Console.WriteLine($"----------------------------------");

            Console.Write("\nConfirm Reservation? (Y/N): ");
            char confirmation = Char.ToUpper(Console.ReadLine()![0]);
            while (confirmation != 'Y' && confirmation != 'N') 
            {
                Console.WriteLine("Please input Y/N only.");
                Console.Write("\nConfirm Reservation? (Y/N): ");
                confirmation = Char.ToUpper(Console.ReadLine()![0]);
            }
            return (confirmation == 'Y');

        }

        /// <summary>
        /// Retrieves the available flights open for reservation.
        /// </summary>
        /// <param name="airlineCode"></param>
        /// <param name="flightNumber"></param>
        /// <returns>List of flights based on airline code and flight number.</returns>
        public static List<Flight> CheckFlightAvailability(string airlineCode, int flightNumber)
        {
            List<Flight> searchResult;
            searchResult = (from flight in FlightMaintenanceBusinessLogic.flightsList
                            where flight.AirlineCode == airlineCode && flight.FlightNumber == flightNumber
                            select flight).ToList();

            Console.WriteLine($"\n*******FLIGHTS WITH AIRLINE CODE {airlineCode} AND FLIGHT NUMBER {flightNumber}*******");

            FlightMaintenanceBusinessLogic.DisplayFlightSearchResults(searchResult);

            return searchResult;
        }

        /// <summary>
        /// Generates the input fields where the user can enter all passenger details.
        /// </summary>
        /// <param name="numberOfPassengers"></param>
        /// <returns>List of the passengers with their info.</returns>
        public static List<Passenger> InputPassengerDetails(int numberOfPassengers)
        {
            List<Passenger> passengers = new();

            for (int i = 0; i < numberOfPassengers; i++)
            {
                Console.WriteLine($"***Enter Passenger {i + 1} Details***");
                string firstName = InputValidation.GetPassengerName($"Enter Passenger {i + 1} First Name: ");
                string lastName = InputValidation.GetPassengerName($"Enter Passenger {i + 1} Last Name: ");
                DateTime birthDate = InputValidation.ValidateDateFormat($"Enter Passenger {i + 1} Birth Date (MM/dd/yyyy): ");
                while (!(InputValidation.CheckIfPastDated(birthDate)))
                {
                    Console.WriteLine("Birth date must be past-dated.");
                    birthDate = InputValidation.ValidateDateFormat($"Enter Passenger {i + 1} Birth Date (MM/dd/yyyy): ");
                }

                int age = CalculateBirthDay(birthDate);
                Console.WriteLine($"Age of the Passenger is {age}.");
                passengers.Add(new Passenger(firstName, lastName, birthDate, age));
            }

            return passengers;
        }

        /// <summary>
        /// Calculates the age of the passenger based on the provided birthday.
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns>Int of Age</returns>
        public static int CalculateBirthDay(DateTime birthDate) 
        {
            return (DateTime.Now - birthDate).Days / 365;
        }

        /// <summary>
        /// Generates a unique PNR used to identify a specific reservation.
        /// </summary>
        /// <returns>String of 6 Alphanumeric Characters where the first character is a letter.</returns>
        public static string GeneratePnrNumber()
        {
            Random random = new Random();
            StringBuilder firstChar = new StringBuilder();
            StringBuilder randPnr = new StringBuilder();
            string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string validFirstChar = validChars.Substring(0, 26);

            firstChar.Append(validFirstChar[random.Next(validFirstChar.Length)]);

            for (int i = 0; i < 5; i++)
            {
                randPnr.Append(validChars[random.Next(validChars.Length)]);
            }

            return firstChar.Append(randPnr).ToString();
        }

        /// <summary>
        /// Determines if a similar has already been made to avoid duplications.
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns>List of bookings with the same PNR if any.</returns>
        public static List<Booking> SearchDuplicatePnr(string pnr) 
        {
            List<Booking> searchResult;
            searchResult = (from booking in bookingsList
                            where booking.PNR == pnr
                            select booking).ToList();

            return searchResult;
        }

        /// <summary>
        /// Displays the results of the reservation query in a tabular format.
        /// </summary>
        /// <param name="list"></param>
        public static void DisplayBookingSearchResults(List<Booking> list)
        {
            Console.WriteLine("\n**********LIST OF ALL RESERVATIONS**********");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|{0,-8}|{1,-15}|{2,-15}|{3,-10}|{4,-12}|{5,-15}|{6,-18}|{7,-15}|{8,-10}", "PNR", "Airline Code", "Flight Number", "Origin", "Destination", "Time Of Arrival", "Time Of Departure", "Flight Date", "Passenger #|");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");

            foreach (Booking booking in list)
            {
                Console.WriteLine("|{0,-8}|{1,-15}|{2,-15}|{3,-10}|{4,-12}|{5,-15}|{6,-18}|{7,-15}|{8,-10} |", booking.PNR, booking.FlightInfo.AirlineCode, booking.FlightInfo.FlightNumber, booking.FlightInfo.DepartureStation, booking.FlightInfo.ArrivalStation, booking.FlightInfo.STA.ToString("HH:mm"), booking.FlightInfo.STD.ToString("HH:mm"), booking.FlightDate.ToString("MM/dd/yyyy"), booking.NumberOfPassengers);

            }

            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
        }
    }
}
