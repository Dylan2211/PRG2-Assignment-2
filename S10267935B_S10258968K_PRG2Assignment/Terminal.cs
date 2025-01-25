using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace S10267935B_S10258968K_PRG2Assignment
{
    class Terminal
    {
        //Properties
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }
        public Dictionary<string, BoardingGate> BoardingGates { get; set; }
        public Dictionary<string, double> GateFees { get; set; }
        //Methods
        public bool AddFlight(string flightCode, Flight flight)
        {
            foreach (var existingFlight in Flights.Values)
            {
                if (existingFlight.FlightNumber == flightCode)
                {
                    Console.WriteLine($"Error: Flight {flightCode} already exists!");
                    return false;
                }
            }

            Flights.Add(flightCode, flight);
            Console.WriteLine($"Flight {flightCode} added successfully.");
            return true;
        }
        //Constructor
        public Terminal(string terminalName, Dictionary<string, Airline> airlines, Dictionary<string, Flight> flights, Dictionary<string, BoardingGate> boardingGates, Dictionary<string, double> gatefees)
        {
            TerminalName = terminalName;
            Airlines = airlines;
            Flights = flights;
            BoardingGates = boardingGates;
            GateFees = gatefees;
        }
    }
}
