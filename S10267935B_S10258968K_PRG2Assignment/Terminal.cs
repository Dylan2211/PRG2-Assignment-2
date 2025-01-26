using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
// Ying Zhi
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
        // Add an airline to the terminal
        public bool AddAirline(Airline airline)
        {
            string airlineCode = airline.Code;
            if (Airlines.ContainsKey(airlineCode))
            {
                Console.WriteLine($"Airline {airlineCode} already exists.");
                return false;
            }
            Airlines[airlineCode] = airline;
            Console.WriteLine($"Airline {airlineCode} added.");
            return true;
        }
        // Add a boarding gate to the terminal
        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            string gateCode = boardingGate.GateName;
            if (BoardingGates.ContainsKey(gateCode))
            {
                Console.WriteLine($"Boarding Gate {gateCode} already exists.");
                return false;
            }
            BoardingGates[gateCode] = boardingGate;
            Console.WriteLine($"Boarding Gate {gateCode} added.");
            return true;
        }
        // Get the name of the Airline from the Flight
        public Airline GetAirlineFromFlight(Flight flight)
        {
            if (airline.ContainsKey(flightCode))
            {
            }
        }
        // Get the Airline fees
        public void PrintAirlineFees()
        {
            if (GateFees.Count == 0)
            {
                Console.WriteLine("No gate fees available.");
                return;
            }

        }
        public Airline GetAirlineFromFlight(Flight flight)
        {
            if (flight != null)
            {
                foreach (var airline in Airlines.Values)
                {
                    if (airline.Flights.ContainsKey(flight.FlightNumber))
                    {
                        return airline;
                    }
                }
                Console.WriteLine("No airline can be found.");
            }
            else
            {
                Console.WriteLine("Wrong flight.");
            }

            return null;
        }
        // ToString
        public override string ToString()
        {
            return $"Terminal Name: {TerminalName,-10} Airlines: {Airlines.Count} Flights: {Flights.Count} Boarding Gates: {BoardingGates.Count}";
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

        //public bool AddFlight(string flightCode, Flight flight)
        //{
        //    foreach (var existingFlight in Flights.Values)
        //    {
        //        if (existingFlight.FlightNumber == flightCode)
        //        {
        //            Console.WriteLine($"Error: Flight {flightCode} already exists!");
        //            return false;
        //        }
        //    }

        //    Flights.Add(flightCode, flight);
        //    Console.WriteLine($"Flight {flightCode} added successfully.");
        //    return true;
        //}
