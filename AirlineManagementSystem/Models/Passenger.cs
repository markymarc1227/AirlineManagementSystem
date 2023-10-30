using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem.Models
{
    public class Passenger
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }

        public Passenger(string firstName, string lastName, DateTime birthDate, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Age = age;
        }
    }
}
