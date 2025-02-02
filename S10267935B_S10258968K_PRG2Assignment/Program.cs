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
    Console.WriteLine("8. Process Unassigned Flights");
    Console.WriteLine("9. Display Total Fee per Airline Fee Summary");
    Console.WriteLine("0. Exit");
    Console.WriteLine("=============================================");
    Console.Write("Please select your option: ");
            
    string choice = Console.ReadLine().Trim();
    Console.WriteLine();

    // Process user's choice
    if (choice == "1")
    {
        Console.WriteLine("\r\n=============================================\r\nList of Flights for Changi Airport Terminal 5\r\n=============================================");
        // List All Flights
        PrintAllFlightsDetails();
    }
    else if (choice == "2")
    {
        Console.WriteLine("=============================================\r\nList of Boarding Gates for Changi Airport Terminal 5\r\n=============================================");
        // List Boarding Gates
        ListAllBoardingGates();
    }
    else if (choice == "3")
    {
        Console.WriteLine("\r\n=============================================\r\nAssign a Boarding Gate to a Flight\r\n=============================================");
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
        Console.WriteLine("=============================================\r\nFlight Schedule for Changi Airport Terminal 5\r\n=============================================\r\n");
        // Display Flight Schedule
        DisplayScheduledFlights();
    }
    else if (choice == "8")
    {
        // Process all unassigned flights to boarding gates in bulk
        ProcessUnassignedFlightsBulk();
    }
    else if (choice == "9")
    {
        // Display Total Fee per Airline Fee Summary
        DisplayTotalFeePerAirlineFeeSummary();
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
    foreach (var entry in FlightDictionary)
    {
        Flight flight = entry.Value;
        Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-20}{"Origin",-20}{"Destination",-20}{"Expected Departure/Arrival Time",-30}");
        Console.WriteLine(flight.ToString(AirlineList));
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

// 10. Process all unassigned flights to boarding gates in bulk
ProcessUnassignedFlightsBulk();

void ProcessUnassignedFlightsBulk()
{
    // Step 1: Find all unassigned flights
    Queue<Flight> unassignedFlights = new Queue<Flight>();
    foreach (var flight in FlightDictionary.Values)
    {
        bool isAssigned = false;
        foreach (var gate in BoardingGate.Values)
        {
            if (gate.Flight == flight)
            {
                isAssigned = true;
                break;
            }
        }

        if (!isAssigned)
        {
            unassignedFlights.Enqueue(flight);
        }
    }
    Console.WriteLine($"\nFlights needing gates: {unassignedFlights.Count}");

    // Step 2: Count unassigned gates
    int unassignedGates = 0;
    foreach (var gate in BoardingGate.Values)
    {
        if (gate.Flight == null)
        {
            unassignedGates++;
        }
    }
    Console.WriteLine($"Available gates: {unassignedGates}");

    int processedFlights = 0;
    int processedGates = 0;

    // Step 3: Process each flight
    while (unassignedFlights.Count > 0)
    {
        Flight currentFlight = unassignedFlights.Dequeue();
        string requestCode = GetRequestCode(currentFlight);

        BoardingGate bestGate = null;

        // Step 4: Find matching gate
        foreach (var gate in BoardingGate.Values)
        {
            if (gate.Flight != null) continue;

            if (requestCode == "CFFT" && gate.SupportsCFFT)
            {
                bestGate = gate;
                break;
            }
            else if (requestCode == "DDJB" && gate.SupportsDDJB)
            {
                bestGate = gate;
                break;
            }
            else if (requestCode == "LWTT" && gate.SupportsLWTT)
            {
                bestGate = gate;
                break;
            }
            else if (requestCode == "None" &&
                     !gate.SupportsCFFT &&
                     !gate.SupportsDDJB &&
                     !gate.SupportsLWTT)
            {
                bestGate = gate;
                break;
            }
        }

        // Step 5: Assign gate if found
        if (bestGate != null)
        {
            bestGate.Flight = currentFlight;
            processedFlights++;
            processedGates++;

            // Display assignment details
            Console.WriteLine($"\nAssigned {currentFlight.FlightNumber}:");
            Console.WriteLine($"- Request: {requestCode}");
            Console.WriteLine($"- Gate: {bestGate.GateName}");
            Console.WriteLine($"- Supports CFFT: {bestGate.SupportsCFFT}");
            Console.WriteLine($"- Supports DDJB: {bestGate.SupportsDDJB}");
            Console.WriteLine($"- Supports LWTT: {bestGate.SupportsLWTT}");
        }
    }

    // Step 6: Show results
    Console.WriteLine($"\nTotal flights assigned: {processedFlights}");
    Console.WriteLine($"Total gates used: {processedGates}");
}

string GetRequestCode(Flight flight)
{
    if (flight is CFFTFlight) return "CFFT";
    if (flight is DDJBFlight) return "DDJB";
    if (flight is LWTTFlight) return "LWTT";
    return "None";
}

// Add this helper method to map Special Request Codes to additional fees.
// (Adjust the fee amounts as required by your specifications.)
decimal GetAdditionalFee(string specialRequestCode)
{
    switch (specialRequestCode)
    {
        case "CFFT":
            return 100m; // Example fee for CFFT
        case "DDJB":
            return 200m; // Example fee for DDJB
        case "LWTT":
            return 300m; // Example fee for LWTT
        default:
            return 0m;
    }
}


// 10. Display the total fee per airline for the day.

void DisplayTotalFeePerAirlineFeeSummary()
{
    // STEP 1: Check that all Flights have been assigned Boarding Gates.
    List<Flight> unassignedFlights = new List<Flight>();
    foreach (Flight flight in FlightDictionary.Values)
    {
        bool isAssigned = false;
        foreach (BoardingGate gate in BoardingGate.Values)
        {
            if (gate.Flight == flight)
            {
                isAssigned = true;
                break;
            }
        }
        if (!isAssigned)
            unassignedFlights.Add(flight);
    }

    if (unassignedFlights.Count > 0)
    {
        Console.WriteLine("ERROR: Some flights have not been assigned Boarding Gates.");
        Console.WriteLine("Please assign Boarding Gates for all flights before running this feature again.");
        return;
    }

    // Initialize overall totals.
    decimal overallSubtotalFees = 0m;
    decimal overallSubtotalDiscounts = 0m;

    // STEP 2: Process each Airline.
    foreach (Airline airline in AirlineList)
    {
        if (airline.Flights == null || airline.Flights.Count == 0)
        {
            Console.WriteLine($"Airline {airline.Name} has no flights scheduled.");
            continue;
        }

        decimal airlineSubtotalFees = 0m;
        decimal airlineSubtotalDiscounts = 0m;

        // Process each flight for the airline.
        foreach (Flight flight in airline.Flights.Values)
        {
            decimal fee = 0m;

            // If the flight's Origin is Singapore, add $800.
            if (flight.Origin == "SIN")
                fee += 800m;
            // If the flight's Destination is Singapore, add $500.
            if (flight.Destination == "SIN")
                fee += 500m;

            // Determine the special request code based on the flight's type.
            string requestCode = GetRequestCode(flight);
            // If there is a Special Request Code (and it isn’t "None"), add the additional fee.
            if (!string.IsNullOrWhiteSpace(requestCode) && requestCode != "None")
                fee += GetAdditionalFee(requestCode);

            // Apply the Boarding Gate Base Fee.
            fee += 300m;

            // Accumulate this flight's fee.
            airlineSubtotalFees += fee;

            // Compute discount based on promotional conditions.
            decimal discount = ComputeDiscount(flight);
            airlineSubtotalDiscounts += discount;
        }

        // Calculate the final fee for the airline.
        decimal airlineFinalFee = airlineSubtotalFees - airlineSubtotalDiscounts;

        // Display the breakdown for this airline.
        Console.WriteLine($"Airline: {airline.Name}");
        Console.WriteLine($"  Original Subtotal Fees: ${airlineSubtotalFees:N2}");
        Console.WriteLine($"  Subtotal Discounts:     ${airlineSubtotalDiscounts:N2}");
        Console.WriteLine($"  Final Fee Charged:      ${airlineFinalFee:N2}");
        Console.WriteLine(new string('-', 40));

        overallSubtotalFees += airlineSubtotalFees;
        overallSubtotalDiscounts += airlineSubtotalDiscounts;
    }

    // Compute overall totals.
    decimal overallFinalFees = overallSubtotalFees - overallSubtotalDiscounts;
    decimal discountPercentage = overallFinalFees != 0
        ? (overallSubtotalDiscounts / overallFinalFees) * 100
        : 0;

    // Display the overall totals.
    Console.WriteLine("Overall Totals for Terminal 5:");
    Console.WriteLine($"  Total Original Fees: ${overallSubtotalFees:N2}");
    Console.WriteLine($"  Total Discounts:     ${overallSubtotalDiscounts:N2}");
    Console.WriteLine($"  Total Final Fees:    ${overallFinalFees:N2}");
    Console.WriteLine($"  Discount Percentage: {discountPercentage:N2}%");
}

decimal ComputeDiscount(Flight flight)
{
    decimal discount = 0m;

    // Promotion 1: For flights arriving/departing before 11am or after 9pm
    if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour >= 21)
        discount += 110m;

    // Promotion 2: If the flight's origin is DXB, BKK, or NRT
    if (flight.Origin == "DXB" || flight.Origin == "BKK" || flight.Origin == "NRT")
        discount += 25m;

    // Promotion 3: If the flight does not indicate a Special Request Code
    if (GetRequestCode(flight) == "None")
        discount += 50m;

    return discount;
}
