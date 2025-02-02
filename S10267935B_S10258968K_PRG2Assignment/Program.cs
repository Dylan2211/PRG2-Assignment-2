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
    //Me and dylan had a misscoummication so he ended up accidentally committing this when i asked him to check my code.
    // Assigning the flight to the dictionary based on status
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

while (true)
{
    // Display Menu
    Console.WriteLine("=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Modify Flight Details");
    Console.WriteLine("7. Display Flight Schedule");
    Console.WriteLine("0. Exit");
    Console.WriteLine("=============================================");
    Console.Write("Please select your option: ");
            
    string choice = Console.ReadLine().Trim();
    Console.WriteLine();

    // Process user's choice
    if (choice == "1")
    {
        // List All Flights
        PrintAllFlightsDetails();
    }
    else if (choice == "2")
    {
        // List Boarding Gates
        ListAllBoardingGates();
    }
    else if (choice == "3")
    {
        // Assign a Boarding Gate to a Flight
        AssignBoardingGateToFlight();
    }
    else if (choice == "4")
    {
        // Create Flight
        CreateNewFlight();
    }
    else if (choice == "5")
    {
        // Display Airline Flights
        DisplayFlightDetails();
    }
    else if (choice == "6")
    {
        // Modify Flight Details
        ModifyFlightDetails();
    }
    else if (choice == "7")
    {
        // Display Flight Schedule
        DisplayScheduledFlights();
    }
    else if (choice == "0")
    {
        // Exit the program
        Console.WriteLine("Goodbye!");
        break; // Exit the loop and terminate the program
    }
    else
    {
        Console.WriteLine("Invalid option. Please select a valid option.");
    }
}





// 3. Print all flight details
void PrintAllFlightsDetails()
{
    // Iterating through the FlightDictionary and printing each flight's details
    foreach (var entry in FlightDictionary)
    {
        Flight flight = entry.Value;
        Console.WriteLine(flight);
    }
}


// 4. List all boarding gates

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

// 5 Assign a boarding gate to a flight


void AssignBoardingGateToFlight()
{
    Flight selectedFlight = null;
    BoardingGate selectedGate = null;

    // Prompt for Flight Number and validate input
    while (true)
    {
        Console.WriteLine("Enter Flight Number:");
        string flightNumber = Console.ReadLine().Trim();
        if (FlightDictionary.TryGetValue(flightNumber, out selectedFlight))
        {
            break;
        }
        Console.WriteLine("Flight not found. Please try again.");
    }

    // Determine special request code based on flight type
    string GetSpecialRequestCode(Flight flight)
    {
        if (flight is CFFTFlight)
        {
            return "CFFT";
        }
        else if (flight is DDJBFlight)
        {
            return "DDJB";
        }
        else if (flight is LWTTFlight)
        {
            return "LWTT";
        }
        else if (flight is NORMFlight)
        {
            return "None";
        }
        else
        {
            return "Unknown";
        }
    }

    // Display selected flight details
    string src = GetSpecialRequestCode(selectedFlight);
    Console.WriteLine($"\nFlight Number: {selectedFlight.FlightNumber}");
    Console.WriteLine($"Origin: {selectedFlight.Origin}");
    Console.WriteLine($"Destination: {selectedFlight.Destination}");
    Console.WriteLine($"Expected Time: {selectedFlight.ExpectedTime:dd/MM/yyyy hh:mm tt}");
    Console.WriteLine($"Special Request Code: {src}");

    // Prompt for Boarding Gate and validate input
    while (true)
    {
        Console.WriteLine("\nEnter Boarding Gate Name:");
        string gateName = Console.ReadLine().Trim();

        if (!BoardingGate.TryGetValue(gateName, out selectedGate))
        {
            Console.WriteLine("Invalid boarding gate. Please try again.");
            continue;
        }

        // Check if the gate is already occupied
        if (selectedGate.Flight != null)
        {
            Console.WriteLine($"Boarding Gate {gateName} is already occupied by flight {selectedGate.Flight.FlightNumber}");
            continue;
        }

        break;
    }

    // Assign flight to the selected boarding gate
    selectedGate.Flight = selectedFlight;

    // Display updated flight and gate details
    Console.WriteLine($"Flight Number: {selectedFlight.FlightNumber}");
    Console.WriteLine($"Special Request Code: {src}");
    Console.WriteLine($"Boarding Gate: {selectedGate.GateName}");
    Console.WriteLine($"Supports DDJB: {selectedGate.SupportsDDJB}");
    Console.WriteLine($"Supports CFFT: {selectedGate.SupportsCFFT}");
    Console.WriteLine($"Supports LWTT: {selectedGate.SupportsLWTT}");

    // Ask if the user wants to update the flight status
    Console.WriteLine("\nWould you like to update the flight status? (Y/N)");
    if (Console.ReadLine().Trim().ToUpper() == "Y")
    {
        // Display available status options
        Console.WriteLine("1. Delayed");
        Console.WriteLine("2. Boarding");
        Console.WriteLine("3. On Time");
        Console.Write("Please select the new status of the flight: ");
        string choice = Console.ReadLine().Trim();

        // Set the new status based on user input
        if (choice == "1")
        {
            selectedFlight.Status = "Delayed";
        }
        else if (choice == "2")
        {
            selectedFlight.Status = "Boarding";
        }
        else if (choice == "3")
        {
            selectedFlight.Status = "On Time";
        }
        else
        {
            selectedFlight.Status = "On Time";
        }
    }
    else
    {
        // Default status if no update is made
        selectedFlight.Status = "On Time";
    }

    // Confirm that the flight has been assigned to the boarding gate
    Console.WriteLine($"\nFlight {selectedFlight.FlightNumber} has been assigned to Boarding Gate {selectedGate.GateName}!");
}


// 6. Create a new flight


void CreateNewFlight()
{
    while (true)
    {
        try
        {
            // Prompt user to input flight details
            Console.Write("Enter Flight Number: ");
            string FlightNumber = Console.ReadLine().Trim();

            Console.Write("Enter Origin: ");
            string Origin = Console.ReadLine().Trim();

            Console.Write("Enter Destination: ");
            string Destination = Console.ReadLine().Trim();

            Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
            DateTime ExpectedTime = DateTime.Parse(Console.ReadLine().Trim());

            Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
            string Status = Console.ReadLine().Trim().ToUpper();
            if (Status == "NONE")
                Status = "";  // If no special request, set to empty

            // Create specific flight type based on status
            if (Status == "CFFT")
            {
                FlightDictionary[FlightNumber] = new CFFTFlight(FlightNumber, Origin, Destination, ExpectedTime, Status);
            }
            else if (Status == "DDJB")
            {
                FlightDictionary[FlightNumber] = new DDJBFlight(FlightNumber, Origin, Destination, ExpectedTime, Status);
            }
            else if (Status == "LWTT")
            {
                FlightDictionary[FlightNumber] = new LWTTFlight(FlightNumber, Origin, Destination, ExpectedTime, Status);
            }
            else // Default to normal flight if no special request
            {
                FlightDictionary[FlightNumber] = new NORMFlight(FlightNumber, Origin, Destination, ExpectedTime, Status);
            }

            // Write flight details to CSV file
            string csvLine = $"{FlightNumber},{Origin},{Destination},{ExpectedTime:h:mm tt},{Status}";
            File.AppendAllText("flights.csv", "\r\n" + csvLine);

            // Confirmation message
            Console.WriteLine($"\nFlight {FlightNumber} has been added!");

            // Ask if user wants to add another flight
            Console.Write("\nWould you like to add another flight? (Y/N) ");
            if (Console.ReadLine().Trim().ToUpper() != "Y")
                break;  // Exit the loop if the user doesn't want to add more flights
        }
        catch (Exception e)
        {
            // Error handling in case of invalid input
            Console.WriteLine($"Error: {e.Message}");
            Console.WriteLine("Please try again.");
        }
    }
}


// 7. Display full flight details from an airline

void DisplayFlightDetails()
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
            return;
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
            return;
        }
        // Display all flights from the airline
        Console.WriteLine(flight + "Airline Name: " + airline.Name);
        return;
    }
    catch (Exception e)
    {
        Console.WriteLine("An error occurred: " + e.Message);
    }
}

// 8. Modify flight details

void ModifyFlightDetails()
{
    try
    {
        //List all airlines available
        foreach (Airline a in AirlineList)
        {
            Console.WriteLine(a);
        }

        //Prompt user for airline code
        Airline airline = null;

        //Retrieve airline object
        while (airline == null)
        {
            Console.Write("Please enter the Airline Code: ");
            string airline_code = Console.ReadLine();
            foreach (Airline a in AirlineList)
            {
                if (a.Code == airline_code)
                {
                    airline = a;
                    break;
                }
            }
            if (airline == null)
            {
                Console.WriteLine("Error: Airline not found. Please try again.");
            }
        }

        //Display Flight Airline Number, Origin, Destination
        if (!AirlineList.Any()) 
        {
            Console.WriteLine("No flights available.");
            return;
        }
        foreach (Flight f in airline.Flights.Values)
        {
            Console.WriteLine($"Flight Number: {f.FlightNumber,-7} Origin: {f.Origin,-15} Destination: {f.Destination,-15}");
        }

        // Prompt user for Option
        string input = "";
        while (input != "1" || input != "2")
        {
            Console.WriteLine("Please enter [1] to modify an existing flight and [2] to choose an existing flight to delete: ");
            input = Console.ReadLine();
            if (input == "1" || input == "2")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        // 2 options
        if (input == "1")
        {
            Flight flight = null;
            //Retrieve flight object
            while (flight == null)
            {
                Console.WriteLine("Please enter the Flight Number: ");
                string flightNumber = Console.ReadLine();
                foreach (Flight f in FlightDictionary.Values)
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
            }
            Console.WriteLine("Please enter the new Origin: ");
            string newOrigin = Console.ReadLine();
            Console.WriteLine("Please enter the new Destination: ");
            string newDestination = Console.ReadLine();
            Console.WriteLine("Please enter the new Expected Time (dd/mm/yyyy hh:mm): ");
            DateTime newExpectedTime = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the new Status (if any): ");
            string newStatus = Console.ReadLine();
            Console.WriteLine("Please enter the new Boarding Gate (if any): ");
            string newBoardingGate = Console.ReadLine();
            Console.WriteLine("Pleae enter the new Special Request Codes (if any): ");
            string newSpecialRequest = Console.ReadLine().ToUpper();

            //flight.Origin = newOrigin;
            //flight.Destination = newDestination;
            //flight.ExpectedTime = newExpectedTime;
            //flight.Status = newStatus;

            if (newBoardingGate != "")
            {
                // Find old boarding gate
                BoardingGate oldBoardingGate = null;
                foreach (BoardingGate bg in BoardingGate.Values)
                {
                    if (bg.Flight.FlightNumber == flight.FlightNumber)
                    {
                        oldBoardingGate = bg;
                        break;
                    }
                }

                // Check if old boarding gate exists
                if (oldBoardingGate != null)
                {
                    oldBoardingGate.Flight = null;
                }

                // Find new boarding gate
                string gate = null;
                foreach (string gatename in BoardingGate.Keys)
                {
                    if (gatename == newBoardingGate)
                    {
                        gate = gatename;
                        break;
                    }
                }

                // Check if new boarding gate exists
                if (gate == null)
                {
                    Console.WriteLine("Error: Boarding Gate not found. Please try again.");
                    return;
                }
                else
                {
                    BoardingGate[gate].Flight = flight;
                }
                // Remove flight from old boarding gate
                if (oldBoardingGate != null)
                {
                    oldBoardingGate.Flight = null;
                }
            }

            // Assigning Flight according to Special Request Codes
            string FlightNumber = flight.FlightNumber;
            if (string.IsNullOrWhiteSpace(newSpecialRequest) )
            {
                FlightDictionary[FlightNumber] = new NORMFlight(FlightNumber, newOrigin, newDestination, newExpectedTime, newStatus);
                //flight.Origin = newOrigin;
                //flight.Destination = newDestination;
                //flight.ExpectedTime = newExpectedTime;
                //flight.Status = newStatus;
                //FlightDictionary[flight.FlightNumber] = new NORMFlight(FlightNumber, Origin, Destination, ExpectedTime, Status);
            }
            else if (newSpecialRequest == "CFFT")
            {
                FlightDictionary[flight.FlightNumber] = new CFFTFlight(FlightNumber, newOrigin, newDestination, newExpectedTime, newStatus);
            }
            else if (newSpecialRequest == "DDJB")
            {
                FlightDictionary[flight.FlightNumber] = new DDJBFlight(FlightNumber, newOrigin, newDestination, newExpectedTime, newStatus);
            }
            else if (newSpecialRequest == "LWTT")
            {
                FlightDictionary[flight.FlightNumber] = new LWTTFlight(FlightNumber, newOrigin, newDestination, newExpectedTime, newStatus);
            }
            else
            {
                Console.WriteLine("Invalid Special Request code. Please try again.");
                return;
            }
            Console.WriteLine("Flight details updated successfully.");
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
                return;
            }
            airline.RemoveFlight(flight);
            Console.WriteLine("Flight deleted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid input. Please try again.");
            return;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("An error occurred: " + e.Message);
    }
}



// 9. Display Scheduled Flight
void DisplayScheduledFlights()
{
    // Display table header for flight details
    Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-20} {4,-30} {5,-10} {6,-15}",
                      "Flight Number", "Airline Name", "Origin", "Destination",
                      "Expected Departure/Arrival", "Status", "Boarding Gate");

    // Dictionary to store flights grouped by expected time
    Dictionary<DateTime, List<Flight>> ScheduledFlight = new Dictionary<DateTime, List<Flight>>();

    // Group flights by their expected time
    foreach (Flight flight in FlightDictionary.Values)
    {
        if (!ScheduledFlight.ContainsKey(flight.ExpectedTime))
        {
            ScheduledFlight[flight.ExpectedTime] = new List<Flight>();
        }
        ScheduledFlight[flight.ExpectedTime].Add(flight);
    }

    // Sort the times for display
    var sortedTimes = new List<DateTime>(ScheduledFlight.Keys);
    sortedTimes.Sort();

    // Iterate through sorted flight times and display flight details
    foreach (DateTime time in sortedTimes)
    {
        foreach (Flight flight in ScheduledFlight[time])
        {
            // Extract airline code from flight number
            string airlineCode = "";
            if (flight.FlightNumber.Contains(" "))
            {
                airlineCode = flight.FlightNumber.Split(' ')[0];
            }
            else if (flight.FlightNumber.Length >= 2)
            {
                airlineCode = flight.FlightNumber.Substring(0, 2);
            }

            // Find airline name based on the airline code
            string airlineName = "Unknown";
            foreach (Airline a in AirlineList)
            {
                if (a.Code == airlineCode)
                {
                    airlineName = a.Name;
                    break;
                }
            }

            // Find the boarding gate assigned to the flight
            string gate = "Unassigned";
            foreach (KeyValuePair<string, BoardingGate> gateEntry in BoardingGate)
            {
                if (gateEntry.Value.Flight != null &&
                    gateEntry.Value.Flight.FlightNumber == flight.FlightNumber)
                {
                    gate = gateEntry.Key;
                    break;
                }
            }

            // Output the flight details in a formatted table
            Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-20} {4,-30} {5,-10} {6,-15}",
                              flight.FlightNumber, airlineName, flight.Origin, flight.Destination,
                              time.ToString("dd/MM/yyyy h:mm tt"), flight.Status, gate);
        }
    }
}
