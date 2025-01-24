//==========================================================
// Student Number : S10270149G
// Partner Number : S10266900J
// Student Name   : Orlando Lee Ming Kai
// Partner Name   : Ewe Yoke Kay 
//==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10270149G__PRG2Assignment
{
    class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime) : base(flightNumber, origin, destination, expectedTime) { }
        public double CalculateFees()
        {
            double baseFee = 300;
            if (Destination == "Singapore (SIN)")
            {
                baseFee += 500;
            }
            else if (Origin == "Singapore (SIN)")
            {
                baseFee += 800;
            }
            return baseFee;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
