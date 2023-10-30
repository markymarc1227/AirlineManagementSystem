using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineManagementSystem.Models;
using AirlineManagementSystem.FlightMaintenance;

namespace AirlineManagementSystem.Reservation
{
    public class ReservationScreen
    {
        /// <summary>
        /// Displays the reservation screen and directs to different reservation features.
        /// </summary>
        public static void ShowReservationMenu()
        {
            string? userInput;
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n**********FLIGHT RESERVATION SCREEN**********");

                Console.WriteLine("\n1.Create a Reservation \n2.Show All Reservations\n3.Search Reservation by PNR\n0.Back to Main Menu");

                Console.Write("Your Input: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        FlightReservationBusinessLogic.CreateReservation();
                        break;
                    case "2":
                        FlightReservationBusinessLogic.ShowAllReservations();
                        break;
                    case "3":
                        FlightReservationBusinessLogic.SearchByPNR();
                        break;
                    case "0":
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("!Please select a valid option!");
                        break;
                }
            }
            while (userInput != "0");

        }
        

    }
}
