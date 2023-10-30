using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineManagementSystem.Models;

namespace AirlineManagementSystem.FlightMaintenance
{
    internal class FlightMaintenanceScreen
    {
        /// <summary>
        /// Displays the main flight maintenance screen and routes to different features.
        /// </summary>
        public static void ShowMaintenanceMenu()
        {
            
            string? userInput1;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n**********FLIGHT MAINTENANCE SCREEN**********");

                Console.WriteLine("\n1.Add a flight \n2.Search flight by Flight Number\n3.Search flight by Airline Code\n4.Search by Origin and Destination\n0.Back to Main Menu");

                Console.Write("Your Input: ");
                userInput1 = Console.ReadLine();

                switch (userInput1)
                {
                    case "1":
                        FlightMaintenanceBusinessLogic.AddFlight();
                        break;
                    case "2":
                        FlightMaintenanceBusinessLogic.SearchByFlightNumber();
                        break;
                    case "3":
                        FlightMaintenanceBusinessLogic.SearchByAirlineCode();
                        break;
                    case "4":
                        FlightMaintenanceBusinessLogic.SearchByOriginDestination();
                        break;
                    case "0":
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("!Please select a valid option!");
                        break;
                }
            }
            while (userInput1 != "0");
            

        }

        

    }
}
