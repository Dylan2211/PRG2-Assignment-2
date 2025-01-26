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

        public bool AddBoardingGate(string gateCode, BoardingGate boardingGate)
        {
            if (BoardingGates[gateCode] != null) 
            {
                Console.WriteLine($"Boarding Gate {gateCode} already exists.");
                return false;
            }
            BoardingGates[gateCode] = boardingGate;
            Console.WriteLine($"Boarding Gate {gateCode} added.");
            return true;
        }
        public Airline GetAirlineFromFlight(Airline airline)
        {
            if (airline.ContainsKey(flightCode))
            {
            }
        }
        public void PrintAirlineFees()
        {
            if (GateFees.Count == 0)
            {
                Console.WriteLine("No gate fees available.");
                return;
            }

        }

        //Constructor
        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
            Airlines = new Dictionary<string, Airline>();
            Flights = new Dictionary<string, Flight>();
            BoardingGates = new Dictionary<string, BoardingGate>();
            GateFees = new Dictionary<string, double>();
        }
    }
}
