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
        public string SpecialRequest { get; private set; }

        public Flight(string flightNumber, string airlineCode, string specialRequest)
        {
            FlightNumber = flightNumber;
            AirlineCode = airlineCode;
            SpecialRequest = specialRequest;
        }
    }

}
