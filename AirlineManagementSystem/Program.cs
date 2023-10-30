using AirlineManagementSystem.FlightMaintenance;
using AirlineManagementSystem.Reservation;

namespace AirlineManagementSystem
{
    // ==================================================================================================
    // This file is part of Navitaire.
    // © Navitaire LLC, an Amadeus company. All rights reserved.
    // ==================================================================================================

    internal class Program
    {
        static void Main(string[] args)
        {
            Initializers.InitializeFlightsAndBookings();
            
            Initializers.ShowMainMenu();

            Console.WriteLine("\nClosing Application... ");
        }
    }
}