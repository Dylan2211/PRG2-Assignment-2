//1 Load Files
Dictionary<string, string> AirlineDictionary = new Dictionary<string, string>();
foreach (string line in File.ReadLines("airlines.csv").Skip(1))
{
    string[] splitLine = line.Split(",");
    string Airline_Name = splitLine[0];
    string Airline_Code = splitLine[1];
    AirlineDictionary[Airline_Name] = Airline_Code;
}
