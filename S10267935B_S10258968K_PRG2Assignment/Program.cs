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
    //FlightDictionary[FlightNumber] = new Flight(FlightNumber, Origin, Destination, ExpectedTime, Status);
    if (Status == "CFFT")
    {
        FlightDictionary[FlightNumber] = new CFFTFlight(FlightNumber, Origin, Destination, ExpectedTime, Status);
    }
    if (Status == "DDJB")
    {
        FlightDictionary[FlightNumber] = new DDJBFlight(FlightNumber, Origin, Destination, ExpectedTime, Status);
    }
    if (Status == "LWTT")
    {
        FlightDictionary[FlightNumber] = new LWTTFlight(FlightNumber, Origin, Destination, ExpectedTime, Status);
    }
    if (Status == "")
    {
        FlightDictionary[FlightNumber] = new NORMFlight(FlightNumber, Origin, Destination, ExpectedTime, Status);
    }
}

// 3. Print all flight details
foreach (var entry in FlightDictionary)
{
    Flight flight = entry.Value;
    Console.WriteLine(flight);
    //Console.WriteLine($"{flight.FlightNumber} {flight.Origin} {flight.Destination} {flight.ExpectedTime:dd/MM/yyyy hh:mm tt}");
}

// 4. List all boarding gates
ListAllBoardingGates();
void ListAllBoardingGates()
{
    string[] ddjb = { "A10", "A11", "A12", "A13", "A20", "A21", "A22", "B10", "B11", "B12" };
    string[] cfft = { "B1", "B2", "B3", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "C10", "C11", "C12", "C13", "C14", "C15", "C16", "C17", "C18", "C19", "C20", "C21", "C22" };
    string[] lwtt = { "A1", "A2", "A20", "A21", "A22", "C14", "C15", "C16", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B10", "B11", "B12", "B13", "B14", "B15", "B16", "B17", "B18", "B19", "B20", "B21", "B22" };
    foreach (var entry in BoardingGate)
    {
        List<string> specialRequests = new List<string>();
        BoardingGate gate = entry.Value;

        if (ddjb.Any(gatename => gate.GateName.Equals(gatename)))
            specialRequests.Add("DDJB");
        if (cfft.Any(gatename => gate.GateName.Equals(gatename)))
            specialRequests.Add("CFFT");
        if (lwtt.Any(gatename => gate.GateName.Equals(gatename)))
            specialRequests.Add("LWTT");

        string formattedSpecialRequests = string.Join(", ", specialRequests);
        Console.WriteLine($"{gate.GateName} {gate.SupportsCFFT} {gate.SupportsDDJB} {gate.SupportsLWTT} {formattedSpecialRequests}");
    }
}

// 7. Display full flight details from an airline
DisplayFlightDetails();
void DisplayFlightDetails()
{
    while (true)
    {
        // List all airlines available
        foreach (Airline a in AirlineList)
        {
            Console.WriteLine(a);
        }
        try
        {
            // Prompt user for airline code
            Console.WriteLine("Please enter the 2-Letter Airline code: ");
            string airlineCode = Console.ReadLine();
            // Retrieve airline object
            Airline airline = null;
            foreach (Airline a in AirlineList)
            {
                if (a.Code == airlineCode)
                {
                    airline = a;
                    break;
                }
            }
            if (airline == null)
            {
                Console.WriteLine("Error: Airline not found. Please try again.");
                continue;
            }
            // Display Flight Airline Number, Origin, Destination
            foreach (Flight f in airline.Flights.Values)
            {
                Console.WriteLine($"Flight Number: {f.FlightNumber,-7} Origin: {f.Origin,-15} Destination: {f.Destination,-15}");
            }
            // Prompt user for Flight Number
            Console.WriteLine("Please enter the Flight Number: ");
            string flightNumber = Console.ReadLine();
            // Retrieve flight object
            Flight flight = null;
            foreach (Flight f in airline.Flights.Values)
            {
                if (f.FlightNumber == flightNumber)
                {
                    flight = f;
                    break;
                }
            }
            if (flight == null)
            {
                Console.WriteLine("Error: Flight not found. Please try again.");
                continue;
            }
            // Display all flights from the airline
            Console.WriteLine(flight + "Airline Name: " + airline.Name);
            break;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            continue;
        }
    }
}

// 8. Modify flight details
//List all airlines available
foreach (Airline a in AirlineList)
{
    Console.WriteLine(a);
}
//Prompt user for airline code
string airline_code = Console.ReadLine();
//Retrieve airline object
Airline airline = null;
foreach (Airline a in AirlineList)
{
    if (a.Code == airline_code)
    {
        airline = a;
        break;
    }
}
//Display Flight Airline Number, Origin, Destination
foreach (Flight f in airline.Flights.Values)
{
    Console.WriteLine($"Flight Number: {f.FlightNumber,-7} Origin: {f.Origin,-15} Destination: {f.Destination,-15}");
}
Console.WriteLine("Please enter [1] to modify an existing flight and [2] to choose an existing flight to delete: ");
string input = Console.ReadLine();
if (input == "1")
{
    Console.WriteLine("Please enter the Flight Number: ");
    string flightNumber = Console.ReadLine();
    //Retrieve flight object
    Flight flight = null;
    foreach (Flight f in airline.Flights.Values)
    {
        if (f.FlightNumber == flightNumber)
        {
            flight = f;
            break;
        }
    }
    if (flight == null)
    {
        Console.WriteLine("Error: Flight not found. Please try again.");
    }
    else
    {
        Console.WriteLine("Please enter the new Origin: ");
        string newOrigin = Console.ReadLine();
        Console.WriteLine("Please enter the new Destination: ");
        string newDestination = Console.ReadLine();
        Console.WriteLine("Please enter the new Expected Time: ");
        DateTime newExpectedTime = DateTime.Parse(Console.ReadLine());
        flight.Origin = newOrigin;
        flight.Destination = newDestination;
        flight.ExpectedTime = newExpectedTime;
        Console.WriteLine("Flight details updated successfully.");
    }
}
else if (input == "2")
{
    Console.WriteLine("Please enter the Flight Number: ");
    string flightNumber = Console.ReadLine();
    //Retrieve flight object
    Flight flight = null;
    foreach (Flight f in airline.Flights.Values)
    {
        if (f.FlightNumber == flightNumber)
        {
            flight = f;
            break;
        }
    }
    if (flight == null)
    {
        Console.WriteLine("Error: Flight not found. Please try again.");
    }
    else
    {
        airline.RemoveFlight(flight);
        Console.WriteLine("Flight deleted successfully.");
    }
}