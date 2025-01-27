//==========================================================
// Student Number : S10270149G
// Partner Number : S10266900J
// Student Name   : Orlando Lee Ming Kai
// Partner Name   : Ewe Yoke Kay 
//==========================================================
namespace S10270149G__PRG2Assignment;

// Class representing an airline
class Airline
{
    public string Name { get; set; }  // Full airline name
    public string Code { get; set; }  // Two-letter airline code (e.g., "SQ")
    public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();

    // Constructor to initialize an airline object
    public Airline(string name, string code)
    {
        Name = name;
        Code = code;
    }

    // Add flight to the airline's flight dictionary
    public bool AddFlight(Flight flight)
    {
        if (Flights.ContainsKey(flight.FlightNumber))
            return false;
        Flights[flight.FlightNumber] = flight;
        return true;
    }

    // Remove a flight by its flight number
    public bool RemoveFlight(string flightNumber)
    {
        return Flights.Remove(flightNumber);
    }

    // Returns airline details as a formatted string
    public override string ToString()
    {
        return $"Airline: {Code}, Name: {Name}";
    }
}
