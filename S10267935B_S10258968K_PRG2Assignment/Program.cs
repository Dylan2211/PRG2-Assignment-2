using S10267935B_S10258968K_PRG2Assignment;
//==========================================================
// Student Number	: S10267935B, S10258968K
// Student Name	: Dylan Wong
// Partner Name	: Ying Zhi
//==========================================================

// 1. Load Files
//Airline
List<Airline> AirlineList = new List<Airline>();
foreach (string line in File.ReadLines("airlines.csv").Skip(1))
{
    string[] splitLine = line.Split(",");
    string Airline_Name = splitLine[0];
    string Airline_Code = splitLine[1];
    AirlineList.Add(new Airline(Airline_Name, Airline_Code));
}

//Boarding Gate
Dictionary<string, BoardingGate> BoardingGate = new Dictionary<string, BoardingGate>();
foreach (string line in File.ReadLines("boardinggates.csv").Skip(1))
{
    string[] splitLine = line.Split(",");
    string gateName = splitLine[0];
    bool supportsDDJB = bool.Parse(splitLine[1]);
    bool supportsCFFT = bool.Parse(splitLine[2]);
    bool supportsLWTT = bool.Parse(splitLine[3]);
    BoardingGate.Add(gateName, new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT));
}








//// 2. Loading the flights.csv data into the FlightDictionary
//Dictionary<string, Flight> FlightDictionary = new Dictionary<string, Flight>();
//foreach (string line in File.ReadLines("flights.csv").Skip(1)) // Skipping header
//{
//    string[] splitLine = line.Split(",");
//    string FlightNumber = splitLine[0];
//    string Origin = splitLine[1];
//    string Destination = splitLine[2];
//    DateTime ExpectedTime = DateTime.Parse(splitLine[3]);
//    string Status = splitLine[4];
//    FlightDictionary[FlightNumber] = new Flight(FlightNumber, Origin, Destination, ExpectedTime, Status);
//}

//// 3. Print all flight details
//foreach (var entry in FlightDictionary)
//{
//    Flight flight = entry.Value;
//    Console.WriteLine($"{flight.FlightNumber} {flight.Origin} {flight.Destination} {flight.ExpectedTime:dd/MM/yyyy hh:mm tt}");
//}