using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267935B_S10258968K_PRG2Assignment
{
    class Flight
    {
        //Properties
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
        //Methods
        public double CalculateFees()
        {
            double fee = 0;

            // Base fees for the flight (this can be modified based on further information from the document)
            if (Destination.Contains("SIN"))  // Arriving at Singapore
            {
                fee += 500; 
            }
            if (Origin.Contains("SIN"))  // Departing from Singapore
            {
                fee += 800;
            }

            // Additional fees for special request codes
            if (Status == "DDJB")
                fee += 300;
            else if (Status == "CFFT")
                fee += 150;
            else if (Status == "LWTT")
                fee += 500;

            return fee;
        }
        public override string ToString()
        {
            return $"Flight Number: {FlightNumber,-7} Origin: {Origin,-15} Destination: {Destination,-15} Expected Time: {ExpectedTime:dd/MM/yyyy hh:mm tt}";
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
