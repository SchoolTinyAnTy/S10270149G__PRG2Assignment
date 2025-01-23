using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10270149G__PRG2Assignment
{
    public class Flight
    {
        public string FlightNumber { get; private set; }
        public string AirlineCode { get; private set; }
        public string Origin { get; private set; }
        public string Destination { get; private set; }
        public string Status { get; set; }
        public string SpecialRequest { get; private set; }
        public string BoardingGateName { get; set; }

        // Constructor with 6 parameters
        public Flight(string flightNumber, string airlineCode, string origin, string destination, string specialRequest)
        {
            FlightNumber = flightNumber;
            AirlineCode = airlineCode;
            Origin = origin;
            Destination = destination;
            SpecialRequest = specialRequest;
            Status = "Scheduled"; // Default status
        }
    }
}