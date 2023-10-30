using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem.Models
{
    public class Booking
    {
        public Flight FlightInfo { get; set; }
        public DateTime FlightDate { get; set; }
        public int NumberOfPassengers { get; set; }
        public List<Passenger> Passengers { get; set; }
        public string PNR { get; set; }

        public Booking(Flight flightInfo, DateTime flightDate, int numberOfPassengers, List<Passenger> passengers, string pnr)
        {
            FlightInfo = flightInfo;
            FlightDate = flightDate;
            NumberOfPassengers = numberOfPassengers;
            Passengers = passengers;
            PNR = pnr;
        }
    }
}
