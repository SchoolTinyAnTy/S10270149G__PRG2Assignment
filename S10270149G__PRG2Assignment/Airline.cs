public class Airline
{
    public string Code { get; private set; } // Two-letter code for the airline
    public string Name { get; private set; } // Full name of the airline

    // Constructor to initialize airline attributes
    public Airline(string code, string name)
    {
        Code = code; // Assign airline code
        Name = name; // Assign airline name
    }

    // Converts airline information to a string
    public override string ToString()
    {
        return $"Airline: {Code}, Name: {Name}"; // Return formatted airline details
    }
}
