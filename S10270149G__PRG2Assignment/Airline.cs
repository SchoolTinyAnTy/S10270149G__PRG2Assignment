//==========================================================
// Student Number : S10270149G
// Partner Number : S10266900J
// Student Name   : Orlando Lee Ming Kai
// Partner Name   : Ewe Yoke Kay 
//==========================================================
namespace S10270149G__PRG2Assignment;

// Class representing an airline
public class Airline
{
    private string name;  // Full airline name
    private string code;  // Two-letter airline code (e.g., "SQ")
    private Dictionary<string, Flight> flights = new Dictionary<string, Flight>(); // Dictionary to store flights

    public string Name { get; set; }  // Full airline name
    public string Code { get; set; }  // Two-letter airline code (e.g., "SQ")
    public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>(); // Dictionary to store flights

    // Constructor to initialize an airline object
    public Airline(string name, string code)
    {
        Name = name; // Initialize name
        Code = code; // Initialize code
    }

    // Add flight to the airline's flight dictionary
    public bool AddFlight(Flight flight)
    {
        if (flight == null) throw new ArgumentNullException(nameof(flight)); // Check for null flight
        if (!Flights.ContainsKey(flight.FlightNumber)) // Check if flight already exists
        {
            Flights.Add(flight.FlightNumber, flight); // Add flight to dictionary
            return true; // Return true if flight is added
        }
        Console.WriteLine("Error: Flight already exists."); // Print error message if flight exists
        return false; // Return false if flight is not added
    }

    // Remove a flight by its flight number
    public bool RemoveFlight(string flightNumber)
    {
        if (string.IsNullOrEmpty(flightNumber)) throw new ArgumentNullException(nameof(flightNumber)); // Check for null or empty flight number
        if (Flights.ContainsKey(flightNumber)) // Check if flight exists
        {
            Flights.Remove(flightNumber); // Remove flight from dictionary
            return true; // Return true if flight is removed
        }
        Console.WriteLine("Error: Flight not found."); // Print error message if flight is not found
        return false; // Return false if flight is not removed
    }

    // Returns airline details as a formatted string
    public override string ToString()
    {
        return $"Airline: {Name} ({Code})"; // Return formatted string with airline details
    }
}
