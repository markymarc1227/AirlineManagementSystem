using AirlineManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace AirlineManagementSystem.Validation
{
    /// <summary>
    /// Contains all the methods necessary for validating user input for both flight maintenance and reservations.
    /// </summary>
    public class InputValidation
    {
        #region FlightMaintenance Validation
        public static String GetAirlineCode(string inquiry)
        {  
            Console.Write(inquiry);
            string airlineCode = Console.ReadLine()!;
            while (!ValidateAirlineCode(airlineCode))
            {
                Console.Write(inquiry);
                airlineCode = Console.ReadLine()!;
            }
            return airlineCode;
        }

        public static bool ValidateAirlineCode(string airlineCode) 
        {
            if (String.IsNullOrEmpty(airlineCode)) 
            {
                Console.WriteLine("Blank Airline code is not allowed.");
                return false;
            }
            if (!(airlineCode.Length <= 3 && airlineCode.Length >= 2)) 
            {
                Console.WriteLine("Airline Code must be 2 - 3 characters!");
                return false;
            }

            if (airlineCode.Count(Char.IsDigit) > 1)
            {
                Console.WriteLine("Only a maximum of 1 number is allowed!");
                return false;
            }

            if (!(IsUpperCase(airlineCode) && IsAlphaNumeric(airlineCode)))
            {
                return false;
            }

            return true;
        }

        public static int GetFlightNumber(string inquiry)
        {
            Console.Write(inquiry);
            string flightNumber = Console.ReadLine()!;
            int convertedFlightNumber = ValidateFlightNumber(flightNumber);
            while (convertedFlightNumber == 0)
            {
                Console.Write(inquiry);
                flightNumber = Console.ReadLine()!;
                convertedFlightNumber = ValidateFlightNumber(flightNumber);
            }
            return convertedFlightNumber;
        }

        public static int ValidateFlightNumber(string flightNumber)
        {
            if (String.IsNullOrEmpty(flightNumber))
            {
                Console.WriteLine("Blank Flight Number is not allowed.");
                return 0;
            }

            int convertedFlightNumber;
            if (!(int.TryParse(flightNumber, out convertedFlightNumber))) 
            {
                Console.WriteLine("Please enter a number.");
                return 0;
            }

            if (!(convertedFlightNumber >= 1 && convertedFlightNumber <= 9999))
            {
                Console.WriteLine("Flight number must range between 1 to 9999 only.");
                return 0;
            }
            return convertedFlightNumber;
        }

        public static String GetStation(string inquiry)
        {
            Console.Write(inquiry);
            string station = Console.ReadLine()!;
            while (!ValidateStation(station))
            {
                Console.Write(inquiry);
                station = Console.ReadLine()!;
            }

            return station;
        }

        public static bool ValidateStation(string station) 
        {
            if (String.IsNullOrEmpty(station))
            {
                Console.WriteLine("Blank station code is not allowed.");
                return false;
            }

            if (station.Length != 3)
            {
                Console.WriteLine("Station must be exactly 3 characters.");
                return false;
            }

            if (!(Char.IsLetter(station, 0)))
            {
                Console.WriteLine("First Character must be a letter.");
                return false;
            }

            if (!(IsUpperCase(station) && IsAlphaNumeric(station)))
            {
                return false;
            }

            return true;
        }

        public static DateTime GetArrivalOrDepartureTime(string inquiry)
        {
            Console.Write(inquiry);
            string time = Console.ReadLine()!;

            DateTime confirmedParseTime;
            bool successfulTimeParse = ParseTime(time,out confirmedParseTime);

            while (!(successfulTimeParse))
            {
                Console.WriteLine("Invalid time input. Please follow the HH:mm format.");
                Console.Write(inquiry);
                time = Console.ReadLine()!;
                successfulTimeParse = ParseTime(time, out confirmedParseTime);
            }

            return confirmedParseTime;
        }

        public static bool ParseTime(string time, out DateTime confirmedParsedTime) 
        {
            DateTime parsedTime;
            bool successfulTimeParse = DateTime.TryParseExact(time, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedTime);
            confirmedParsedTime = parsedTime;
            return successfulTimeParse;
        }

        public static bool IsAlphaNumeric(string code) 
        {
            for (int i = 0; i < code.Length; i++)
            {
                if (!Char.IsLetterOrDigit(code, i))
                {
                    Console.WriteLine("Only Alphanumeric Characters are allowed.");
                    return false;
                }
            }
            return true;
        }

        public static bool IsAlphabetical(string code) 
        {
            for (int i = 0; i < code.Length; i++)
            {
                if (!Char.IsLetter(code, i))
                {
                    Console.WriteLine("Only Alphabetical Characters are allowed.");
                    return false;
                }
            }
            return true;
        }

        public static bool IsUpperCase(string code)
        {
            if (code.ToUpper() != code)
            {
                Console.WriteLine("Characters must be uppercase!");
                return false;
            }
            return true;
        }

        #endregion

        public static int ValidateFlightIndex(string inquiry, List<Flight> flights) 
        {
            Console.Write(inquiry);
            string selectedFlightIndex = Console.ReadLine()!;

            int parsedFlightIndex;
            bool successfulIndexParse = int.TryParse(selectedFlightIndex, out parsedFlightIndex);

            while (!(successfulIndexParse))
            {
                Console.WriteLine("Please enter a valid number.");
                Console.Write(inquiry);
                selectedFlightIndex = Console.ReadLine()!;
                successfulIndexParse = int.TryParse(selectedFlightIndex, out parsedFlightIndex);
            }

            parsedFlightIndex--;
            if (parsedFlightIndex >= 0 && parsedFlightIndex < flights.Count())
            {
                return parsedFlightIndex;
            }
            else
            { 
                return -1;
            }
        }

        public static DateTime ValidateDateFormat(string inquiry)
        {
            Console.Write(inquiry);
            string unparsedDate = Console.ReadLine()!;

            DateTime parsedDate;
            bool successfulDateParse = DateTime.TryParseExact(unparsedDate, "MM/dd/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out parsedDate);

            while (!(successfulDateParse))
            {
                Console.WriteLine("Invalid flight date input. Please follow the \"MM/dd/yyyy\" format.");
                Console.Write(inquiry);
                unparsedDate = Console.ReadLine()!;
                successfulDateParse = DateTime.TryParseExact(unparsedDate, "MM/dd/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out parsedDate);
            }

            return parsedDate;
        }

        public static bool CheckIfFutureDated(DateTime flightDate) 
        {
            return flightDate >= DateTime.Today;
        }

        public static int GetPassengerNumber(string inquiry)
        {
            Console.Write(inquiry);
            string passengerNumber = Console.ReadLine()!;
            int convertedPassengerNumber = ValidatePassengerNumber(passengerNumber);
            while (convertedPassengerNumber == 0)
            {
                Console.Write(inquiry);
                passengerNumber = Console.ReadLine()!;
                convertedPassengerNumber = ValidatePassengerNumber(passengerNumber);
            }
            return convertedPassengerNumber;
        }

        public static int ValidatePassengerNumber(string passengerNumber)
        {
            int convertedPassengerNumber;
            if (!(int.TryParse(passengerNumber, out convertedPassengerNumber)))
            {
                Console.WriteLine("Please enter a number.");
                return 0;
            }
            if (convertedPassengerNumber > 5)
            {
                Console.WriteLine("Passenger number has a max limit of 5.");
                return 0;
            }

            if (convertedPassengerNumber < 1)
            {
                Console.WriteLine("Passenger cannot be 0.");
                return 0;
            }
            return convertedPassengerNumber;
        }

        public static String GetPassengerName(string inquiry)
        {
            Console.Write(inquiry);
            string passengerName = Console.ReadLine()!;
            while (!ValidatePassengerName(passengerName))
            {
                Console.Write(inquiry);
                passengerName = Console.ReadLine()!;
            }
            return passengerName;
        }
        
        public static bool ValidatePassengerName(string passengerName)
        {

            if (String.IsNullOrEmpty(passengerName))
            {
                Console.WriteLine("Passenger Name is required!");
                return false;
            }

            if (!(IsAlphabetical(passengerName))) 
            {
                return false;
            }

            if (passengerName.Length > 20)
            {
                Console.WriteLine("Cannot Exceed 20 Characters!");
                return false;
            }
            return true;
        }

        public static bool CheckIfPastDated(DateTime birthDate)
        {
            return birthDate <= DateTime.Today;
        }

        public static String ValidatePnr(string inquiry)
        {
            Console.Write(inquiry);
            string pnr = Console.ReadLine()!;
            while (!CheckPnrConditions(pnr))
            {
                Console.Write(inquiry);
                pnr = Console.ReadLine()!;
            }
            return pnr;
        }

        public static bool CheckPnrConditions(string pnr)
        {
            if (!Char.IsLetter(pnr[0])) 
            {
                Console.WriteLine("PNR Must start with a letter.");
                return false;
            }
            if (pnr.Length != 6)
            {
                Console.WriteLine("PNR Must be 6 Characters.");
                return false;
            }

            if (!(IsUpperCase(pnr) && IsAlphaNumeric(pnr)))
            {
                return false;
            }

            return true;
        }

    }
}
