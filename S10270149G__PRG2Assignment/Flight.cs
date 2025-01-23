public class Flight
{
    public string FlightNumber { get; private set; }
    public string AirlineCode { get; private set; }
    public string Origin { get; private set; }
    public string Destination { get; private set; }
    public string ExpectedDeparture { get; private set; }
    public string SpecialRequest { get; private set; }
    public string BoardingGateName { get; set; } = null!;  // Nullable

    // Add the missing Status property
    public string Status { get; set; }  // Flight status (e.g., "On Time", "Delayed", "Boarding")

    // Constructor with seven parameters including Status
    public Flight(string flightNumber, string airlineCode, string origin, string destination, string expectedDeparture, string specialRequest)
    {
        FlightNumber = flightNumber;
        AirlineCode = airlineCode;
        Origin = origin;
        Destination = destination;
        ExpectedDeparture = expectedDeparture;
        SpecialRequest = specialRequest;
        BoardingGateName = null;
        Status = "On Time";  // Default status to "On Time"
    }
}
