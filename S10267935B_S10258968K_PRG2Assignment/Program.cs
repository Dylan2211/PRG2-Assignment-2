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
foreach (string line in File.ReadLines("flights.csv").Skip(1)) // Skipping header
{
    // Split the line by comma
    string[] splitLine = line.Split(',');

    // Read each field, trim whitespace
    string FlightNo = splitLine[0].Trim();
    string Origin = splitLine[1].Trim();
    string Destination = splitLine[2].Trim();
    DateTime ExpectedTime = DateTime.ParseExact(splitLine[3].Trim(), "h:mm tt", CultureInfo.InvariantCulture);
    string Status = splitLine[4].Trim(); // This is the Special Request Code (CFFT, DDJB, etc.)

    // Create the correct subclass based on Status
    Flight newFlight;
    
    if (Status == "CFFT")
    {
        newFlight = new CFFTFlight(FlightNo, Origin, Destination, ExpectedTime, Status);
    }
    else if (Status == "DDJB")
    {
        newFlight = new DDJBFlight(FlightNo, Origin, Destination, ExpectedTime, Status);
    }
    else if (Status == "LWTT")
    {
        newFlight = new LWTTFlight(FlightNo, Origin, Destination, ExpectedTime, Status);
    }
    else
    {
        newFlight = new NORMFlight(FlightNo, Origin, Destination, ExpectedTime, Status);
    }

    // Add the Flight object to the dictionary
    FlightDictionary.Add(FlightNo, newFlight);
}

// 3. Print all flight details
foreach (var entry in FlightDictionary)
{
    Flight flight = entry.Value;
    Console.WriteLine(flight);
    //Console.WriteLine($"{flight.FlightNumber} {flight.Origin} {flight.Destination} {flight.ExpectedTime:dd/MM/yyyy hh:mm tt}");
}

// 4. List all boarding gates
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
