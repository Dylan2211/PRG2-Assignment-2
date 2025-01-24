using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267935B_S10258968K_PRG2Assignment
{
    class DDJBFlight : Flight
    {
        public DDJBFlight(string flightNumber, string flightOrigin, string flightDestination, DateTime flightExpectedTime, string flightStatus) : base(flightNumber, flightOrigin, flightDestination, flightExpectedTime, flightStatus)
        {
        }
        public double CalculateFees()
        {
            //double fees = 0;
            //if (FlightStatus == "Landed" || FlightStatus == "Departed")
            //{
            //    fees = 0;
            //}
            //else if (FlightStatus == "Cancelled")
            //{
            //    fees = 0;
            //}
            //else if (FlightStatus == "Boarding")
            //{
            //    fees = 50;
            //}
            //else if (FlightStatus == "Check-In")
            //{
            //    fees = 100;
            //}
            //else if (FlightStatus == "Gate-Closed")
            //{
            //    fees = 150;
            //}
            //return fees;
            return 1;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
