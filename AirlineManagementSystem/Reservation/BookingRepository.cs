using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineManagementSystem.Models;

namespace AirlineManagementSystem.Reservation
{
    public class BookingRepository
    {
        /// <summary>
        /// This class creates the instance of the BookingRepository which is responsible 
        /// for gathering the data related to confirmed reservations/bookings.
        /// </summary>
        static BookingRepository instance;
        protected BookingRepository() 
        {
        }
        public static BookingRepository Instance()
        {
            if (instance == null) 
            {
                instance = new BookingRepository();
            }

            return instance;
        }

        public List<Booking> GetBookings()
        {
            List<Booking> bookingList = new List<Booking>();

            bookingList.Add(new Booking(
                new Flight(
                "ABC",
                1234,
                "CEB",
                "MNL",
                new DateTime(2023, 01, 01, 23, 00, 00),
                new DateTime(2023, 01, 01, 22, 00, 00)
                ),
                new DateTime(2023, 10, 30),
                2,
                new List<Passenger>() {
                    new Passenger("John","Doe",new DateTime(2000,09,05),23),
                    new Passenger("Numero","Uno",new DateTime(2001,08,15),22)
                },
                "XYZ3MD"
                ));

            bookingList.Add(new Booking(
                new Flight(
                "ABC",
                1235,
                "SIN",
                "MNL",
                new DateTime(2023, 01, 01, 23, 00, 00),
                new DateTime(2023, 01, 01, 22, 00, 00)
                ),
                new DateTime(2023, 10, 29),
                1,
                new List<Passenger>() {
                    new Passenger("Paul","Smith",new DateTime(2003,09,05),20)
                },
                "ABZ3MD"
                ));

            return bookingList;
        }

    }
}
