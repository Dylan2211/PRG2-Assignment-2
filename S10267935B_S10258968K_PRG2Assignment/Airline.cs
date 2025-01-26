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
    class Airline
    {
        //Properties
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights{ get; set; }
        //Methods
        public bool AddFlight(Flight f)
        {
            if (f == null) return false; // Ensure flight is not null
            if (!Flights.ContainsKey(f.FlightNumber))
            {
                Flights[f.FlightNumber] = f;
                return true;
            }
            return false;
        }
        public double CalculateFees()
        {
            //Calculate total fees for all flights
            double totalFees = 0;
            double discount = 0;
            string[] specificOrigins = { "DXB", "BKK", "NRT" };
            foreach (var flight in Flights.Values)
            {
                totalFees += flight.CalculateFees();
                if (flight.ExpectedTime.TimeOfDay < new TimeSpan(11,0,0) || flight.ExpectedTime.TimeOfDay > new TimeSpan(21, 0, 0))
                {
                    discount += 110;
                }
                if (specificOrigins.Any(code => flight.Origin.Contains(code))) //Complicated lambda expression to check if it includes specificOrigins
                {
                    discount += 25;
                }
                if (flight.Status == "")
                {
                    discount += 50;
                }
            }
            // Promotional Conditions
            if (Flights.Count >= 5)
            {
                totalFees *= 0.97; //3% discount
            }
            if (Flights.Count >= 3) // $350 discount for every 3 flights
            {
                totalFees -= Math.Floor((double)Flights.Count / 3) * 350; 
            }
            totalFees -= discount;

            return totalFees;
        }
        public bool RemoveFlight(Flight f)
        {
            if (f == null) return false; // Ensure flight is not null
            if (Flights.ContainsKey(f.FlightNumber))
            {
                Flights.Remove(f.FlightNumber);
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"Name: {Name,-10} Code: {Code,-10} Flights: {Flights.Count}";
        }
        //Constructors
        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
            Flights = new Dictionary<string, Flight>();
        }
    }
}
