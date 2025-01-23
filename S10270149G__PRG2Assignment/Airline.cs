class Airline
{
    public string Code { get; set; } // Two-letter code for the airline
    public string Name { get; set; } // Full name of the airline

    public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>(); // Store Flight objects as value and FlightNumber as key 

    // Constructor to initialize airline attributes
    public Airline(string code, string name)
    {
        Code = code; // Assign airline code
        Name = name; // Assign airline name
    }

    // Adds a flight to Flights
    public bool AddFlight(Flight flight)
    {
        if (Flights.ContainsValue(flight)) // Check if flight exist
        {
            return false;
        }
        Flights[flight.FlightNumber] = flight; // Add flight to the dictionary Flights
        return true;
    }

    // Remove a flight from Flights
    public bool RemoveFlight(Flight flight)
    {
        if (Flights.ContainsValue(flight)) // Check if flight exist
        {
            Flights.Remove(flight.FlightNumber); // Remove flight from the dictionary Flights
            return true;
        }
        return false;
    }

    // Converts airline information to a string
    public override string ToString()
    {
        return $"Airline: {Code}, Name: {Name}"; // Return formatted airline details
    }
}
