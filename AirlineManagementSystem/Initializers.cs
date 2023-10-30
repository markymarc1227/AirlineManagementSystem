using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineManagementSystem.Models;
using AirlineManagementSystem.FlightMaintenance;
using AirlineManagementSystem.Reservation;

namespace AirlineManagementSystem
{
    public class Initializers
    {
        /// <summary>
        /// Displays the main menu which redirects to the flight maintenance screen, reservation screen, or exit.
        /// </summary>
        public static void ShowMainMenu()
        {
            string userSelection;
            do {
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine("\n**********AIRLINE MANAGEMENT SYSTEM**********");

                Console.WriteLine("\nSelect a screen: \n1.Flight Maintenance Screen\n2.Reservation Screen\n0.Exit Application");

                Console.Write("Your Input: ");

                userSelection = Console.ReadLine()!;

                switch (userSelection)
                {
                    case "1":
                        FlightMaintenanceScreen.ShowMaintenanceMenu();
                        break;
                    case "2":
                        ReservationScreen.ShowReservationMenu();
                        break;
                    case "0":
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("!Please select a valid option!");
                        break;
                }
            }
            while (userSelection != "0");
            
        }

        /// <summary>
        /// Initializes the flights and booking repositories to retrieve pre-existing data.
        /// </summary>
        public static void InitializeFlightsAndBookings() 
        {
            BookingRepository bookingRepository = BookingRepository.Instance();
            FlightReservationBusinessLogic.bookingsList = bookingRepository.GetBookings();

            FlightRepository flightRepository = FlightRepository.Instance();
            FlightMaintenanceBusinessLogic.flightsList = flightRepository.GetFlights();
        }
    }
}
