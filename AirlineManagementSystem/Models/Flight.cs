using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem.Models
{
    public class Flight
    {
        public string AirlineCode { get; set; }
        public int FlightNumber { get; set; }
        public string ArrivalStation { get; set; }
        public string DepartureStation { get; set; }
        public DateTime STA { get; set; }
        public DateTime STD { get; set; }

        public Flight(string airlineCode, int flightNumber, string arrivalStation, string departureStation, DateTime sta, DateTime std)
        {
            AirlineCode = airlineCode;
            FlightNumber = flightNumber;
            ArrivalStation = arrivalStation;
            DepartureStation = departureStation;
            STA = sta;
            STD = std;
        }


    }
}
