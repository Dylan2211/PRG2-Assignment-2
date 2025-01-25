using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //Does this work?d
            double totalFees = 0;
            foreach (var flight in Flights.Values)
            {
                totalFees += flight.CalculateFees();
            }
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
