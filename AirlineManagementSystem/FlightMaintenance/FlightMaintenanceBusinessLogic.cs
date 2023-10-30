using AirlineManagementSystem.Models;
using AirlineManagementSystem.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem.FlightMaintenance
{
    /// <summary>
    /// This class holds all the functionality necessary for managing flights, such as adding and searching through different properties.
    /// </summary>
    public class FlightMaintenanceBusinessLogic
    {
        public static List<Flight> flightsList = new();

        /// <summary>
        /// This method initiates the creation of a flight by asking the user to fill-out the necessary flight details.
        /// </summary>
        public static void AddFlight()
        {
            string airlineCode;
            int flightNumber;
            string arrivalStation;
            string departureStation;
            DateTime sta;
            DateTime std;

            airlineCode = InputValidation.GetAirlineCode("Enter Airline Code: ");
            flightNumber = InputValidation.GetFlightNumber("Enter Flight Number: ");
            departureStation = InputValidation.GetStation("Enter Departure Station: ");
            arrivalStation = InputValidation.GetStation("Enter Arrival Station: ");
            std = InputValidation.GetArrivalOrDepartureTime("Enter Scheduled Time of Departure (HH:mm): ");
            sta = InputValidation.GetArrivalOrDepartureTime("Enter Scheduled Time of Arrival (HH:mm): ");

            Console.WriteLine("\nChecking for a Duplicate Flight...");
            if (CheckForDuplicateFlights(airlineCode, flightNumber, arrivalStation, departureStation))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"The flight {airlineCode} {flightNumber} {departureStation}-{arrivalStation} already exists. Please enter different flight details.");
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("No Duplicate Flight Found!");
                Flight newFlight = new Flight(airlineCode, flightNumber, arrivalStation, departureStation, sta, std);
                flightsList.Add(newFlight);
                Console.WriteLine($"\nFight {airlineCode} {flightNumber} {departureStation}-{arrivalStation} was successfully added!");
            }
        }

        /// <summary>
        /// This method initiates the creation of a flight by asking the user to fill-out the necessary flight details.
        /// </summary>
        public static void SearchByFlightNumber()
        {
            int flightNum;
            flightNum = InputValidation.GetFlightNumber("Enter a flight number: ");

            List<Flight> searchResults;
            searchResults = RetrieveResultsForFlightNumber(flightNum);

            Console.WriteLine($"\n*******FLIGHTS WITH FLIGHT NUMBER {flightNum}*******");
            DisplayFlightSearchResults(searchResults);

        }

        /// <summary>
        /// Returns a list of flights corresponding to the passed flight number.
        /// </summary>
        public static List<Flight> RetrieveResultsForFlightNumber(int flightNum)
        {
            List<Flight> searchResult;
            searchResult = (from flight in flightsList
                            where flight.FlightNumber == flightNum
                            select flight).ToList();
            return searchResult;
        }

        /// <summary>
        /// Displays all the flights based on the entered airline code.
        /// </summary>
        public static void SearchByAirlineCode()
        {
            string airlineCode;
            airlineCode = InputValidation.GetAirlineCode("Enter an airline code: ");

            List<Flight> searchResult;
            searchResult = RetrieveResultsForAirlineCode(airlineCode); 

            Console.WriteLine($"\n*******FLIGHTS WITH AIRLINE CODE {airlineCode}*******");
            DisplayFlightSearchResults(searchResult);

        }

        /// <summary>
        /// Used by the SearchByAirlineCode() method to get a list of flights based on a valid airline code.
        /// </summary>
        /// <param name="airlineCode"></param>
        /// <returns>A list of flights</returns>
        public static List<Flight> RetrieveResultsForAirlineCode(string airlineCode)
        {
            List<Flight> searchResult;
            searchResult = (from flight in flightsList
                            where flight.AirlineCode == airlineCode
                            select flight).ToList();
            return searchResult;
        }

        /// <summary>
        /// Displays all the flights that has the same origin and destination with the input.
        /// </summary>
        public static void SearchByOriginDestination()
        {
            string originCode;
            string destinationCode;

            originCode = InputValidation.GetStation("Enter an origin code: ");
            destinationCode = InputValidation.GetStation("Enter a destination code: ");

            List<Flight> searchResults = RetrieveResultsForOriginAndDestination( destinationCode, originCode);

            Console.WriteLine($"\n*******FLIGHTS WITH ORIGIN {originCode} AND DESTINATION {destinationCode} *******");
            DisplayFlightSearchResults(searchResults);
        }

        /// <summary>
        /// Used by the SearchByOriginDestination() method to get the list of flights from the data storage.
        /// </summary>
        /// <param name="destinationCode"></param>
        /// <param name="originCode"></param>
        /// <returns>List of flights corresponding to the origin and destination.</returns>
        public static List<Flight> RetrieveResultsForOriginAndDestination(string destinationCode, string originCode) 
        {
            List<Flight> searchResult;
            searchResult = (from flight in flightsList
                            where flight.ArrivalStation == destinationCode && flight.DepartureStation == originCode
                            select flight).ToList();
            return searchResult;
        }

        /// <summary>
        /// Looks for duplicate flights to ensure that all newly registered flights are unique.
        /// </summary>
        /// <param name="airlineCode"></param>
        /// <param name="flightNumber"></param>
        /// <param name="arrivalStation"></param>
        /// <param name="departureStation"></param>
        /// <returns>Returns a boolean depending if a duplicate is found or not.</returns>
        public static bool CheckForDuplicateFlights(string airlineCode, int flightNumber, string arrivalStation, string departureStation) 
        {
            List<Flight> searchResult;
            searchResult = (from flight in flightsList
                            where flight.AirlineCode == airlineCode && 
                            flight.FlightNumber == flightNumber && 
                            flight.ArrivalStation == arrivalStation &&
                            flight.DepartureStation == departureStation
                            select flight).ToList();
            return searchResult.Any();
        }

        /// <summary>
        /// Displays the results of the LINQ queries made by the different flight-related methods.
        /// </summary>
        /// <param name="list"></param>
        public static void DisplayFlightSearchResults(List<Flight> list)
        {

            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-18}|", "Flight Index", "Airline Code", "Flight Number", "Origin", "Destination", "Time Of Arrival", "Time Of Departure");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
            for (int i = 0; i < list.Count(); i++)
            {
                var flight = list[i];

                Console.WriteLine("|{0,-15}|{1,-15}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-18}|", i + 1, flight.AirlineCode, flight.FlightNumber, flight.DepartureStation, flight.ArrivalStation, flight.STA.ToString("HH:mm"), flight.STD.ToString("HH:mm"));

            }
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");

        }
    }
}
