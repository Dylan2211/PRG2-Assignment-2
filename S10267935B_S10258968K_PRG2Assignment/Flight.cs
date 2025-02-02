using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number	: S10267935B, S10258968K
// Student Name	: Dylan Wong
// Partner Name	: Ying Zhi
//==========================================================

namespace S10267935B_S10258968K_PRG2Assignment
{
    abstract class Flight : IComparable<Flight>
    {
        //Properties
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
        //Methods
        public virtual double CalculateFees()
        {
            double fee = 300;
            // Base fees for the flight
            if (Destination.Contains("SIN"))  // Arriving at Singapore
            {
                fee += 500; 
            }
            if (Origin.Contains("SIN"))  // Departing from Singapore
            {
                fee += 800;
            }
            return fee;
        }
        public override string ToString()
        {
            return $"Flight Number: {FlightNumber,-7} Origin: {Origin,-15} Destination: {Destination,-15} Expected Time: {ExpectedTime:dd/MM/yyyy hh:mm tt}";
        }

        public int CompareTo(Flight other)
        {
            if (other == null) return 1 ;
            return this.ExpectedTime.CompareTo(other.ExpectedTime);
        }
        //Constructor
        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = status;
        }
    }
}
