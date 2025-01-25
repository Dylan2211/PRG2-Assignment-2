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
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }
        public Dictionary<string, BoardingGate> boardingGates { get; set; }
        public Dictionary<string, double> GateFees { get; set; }
        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
            Airlines = new Dictionary<string, Airline>();
            Flights = new Dictionary<string, Flight>();
            boardingGates = new Dictionary<string, BoardingGate>();
            GateFees = new Dictionary<string, double>();
        }
    }

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
}
    
}
