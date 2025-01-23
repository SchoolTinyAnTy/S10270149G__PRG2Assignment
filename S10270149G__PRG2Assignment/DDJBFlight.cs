using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10270149G__PRG2Assignment
{
    class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }

        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime) : base(flightNumber, origin, destination, expectedTime)
        {
            RequestFee = 300;
        }

        public double CalculateFees()
        {
            double baseFee = 300;
            if (Destination == "Singapore (SIN)")
            {
                baseFee += RequestFee + 500;
            }
            else if (Origin == "Singapore (SIN)")
            {
                baseFee += RequestFee + 800;
            }
            return baseFee;
        }

        public override string ToString()
        {
            return base.ToString() + "Request fee:" + RequestFee;
        }
    }
}
