using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineManagementSystem.Models;

namespace AirlineManagementSystem.FlightMaintenance
{
    /// <summary>
    /// This class creates the instance of the FlightRepository which is responsible 
    /// for gathering the data of flights.
    /// </summary>
    public class FlightRepository
    {
        static FlightRepository instance;
        protected FlightRepository() 
        {
        }

        public static FlightRepository Instance() 
        {
            if (instance == null)
            { 
                instance = new FlightRepository();
            }
            return instance;
        
        }
        public List<Flight> GetFlights()
        {
            List<Flight> flightList = new List<Flight>();

            flightList.Add(new Flight(
                "ABC",
                1234,
                "CEB",
                "MNL",
                new DateTime(2023, 01, 01, 23, 00, 00),
                new DateTime(2023, 01, 01, 22, 00, 00)
                ));

            flightList.Add(new Flight(
                "ABC",
                1235,
                "SIN",
                "MNL",
                new DateTime(2023, 01, 01, 23, 00, 00),
                new DateTime(2023, 01, 01, 22, 00, 00)
                ));

            flightList.Add(new Flight(
                "ABC",
                1235,
                "SIN",
                "CEB",
                new DateTime(2023, 01, 01, 23, 00, 00),
                new DateTime(2023, 01, 01, 22, 00, 00)
                ));

            flightList.Add(new Flight(
                "ABC",
                1235,
                "SIN",
                "AKN",
                new DateTime(2023, 01, 01, 23, 00, 00),
                new DateTime(2023, 01, 01, 22, 00, 00)
                ));

            flightList.Add(new Flight(
                "EFG",
                3456,
                "MNL",
                "CEB",
                new DateTime(2023, 01, 01, 02, 00, 00),
                new DateTime(2023, 01, 01, 01, 00, 00)
                ));

            flightList.Add(new Flight(
                "HIJ",
                9876,
                "MNL",
                "CEB",
                new DateTime(2023, 01, 01, 05, 00, 00),
                new DateTime(2023, 01, 01, 04, 00, 00)
                ));

            return flightList;
        }
    }
}
