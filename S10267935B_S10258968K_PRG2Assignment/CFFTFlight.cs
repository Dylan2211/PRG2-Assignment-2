using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267935B_S10258968K_PRG2Assignment
{
    class CFFTFlight : Flight
    {
        public CFFTFlight(string flightNumber, string flightOrigin, string flightDestination, DateTime flightExpectedTime, string flightStatus) : base(flightNumber, flightOrigin, flightDestination, flightExpectedTime, flightStatus)
        {
        }
        public double CalculateFees()
        {
            return 1;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
