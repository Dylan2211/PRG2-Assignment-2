using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267935B_S10258968K_PRG2Assignment
{
    class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string flightOrigin, string flightDestination, DateTime flightExpectedTime, string flightStatus) : base(flightNumber, flightOrigin, flightDestination, flightExpectedTime, flightStatus)
        {
        }
        public double CalculateFees()
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
