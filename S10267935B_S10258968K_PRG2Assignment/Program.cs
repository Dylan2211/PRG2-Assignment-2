// 1. Load Files
using S10267935B_S10258968K_PRG2Assignment;

List <Airline> AirlineList = new List<Airline>();
foreach (string line in File.ReadLines("airlines.csv").Skip(1))
{
    string[] splitLine = line.Split(",");
    string Airline_Name = splitLine[0];
    string Airline_Code = splitLine[1];
    AirlineList.Add(new Airline(Airline_Name, Airline_Code));
}


Dictionary<string, Flight> Boarding_Gate = new Dictionary<string, Flight>();
foreach (string line in File.ReadLines("flights.csv").Skip(1))
{
    string[] splitLine = line.Split(",");
    string Flight_Number = splitLine[0];
    string Flight_Origin = splitLine[1];
    string Flight_Destination = splitLine[2];
    DateTime Flight_ExpectedTime = DateTime.Parse(splitLine[3]);
    string Flight_Status = splitLine[4];
    Boarding_Gate[Flight_Number] = new Flight(Flight_Number, Flight_Origin, Flight_Destination, Flight_ExpectedTime, Flight_Status);
}
// 2. Loading the flights.csv data into the FlightDictionary
Dictionary<string, Flight> FlightDictionary = new Dictionary<string, Flight>();
foreach (string line in File.ReadLines("flights.csv").Skip(1)) // Skipping header
{
    string[] splitLine = line.Split(",");
    string FlightNumber = splitLine[0];
    string Origin = splitLine[1];
    string Destination = splitLine[2];
    DateTime ExpectedTime = DateTime.Parse(splitLine[3]);
    string Status = splitLine[4];
    FlightDictionary[FlightNumber] = new Flight(FlightNumber, Origin, Destination, ExpectedTime, Status);
}

// 3. Print all flight details
foreach (var entry in FlightDictionary)
{
    Flight flight = entry.Value;
    Console.WriteLine($"{flight.FlightNumber} {flight.Origin} {flight.Destination} {flight.ExpectedTime:dd/MM/yyyy hh:mm tt}");
}

// 4. List all boarding gates
foreach 