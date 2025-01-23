public class Flight
{
    public string FlightNumber { get; private set; }  // Unique identifier for the flight
    public string AirlineCode { get; private set; }  // Airline's two-letter code (e.g., "SQ")
    public string Origin { get; private set; }  // Origin airport of the flight
    public string Destination { get; private set; }  // Destination airport of the flight
    public string ExpectedDeparture { get; private set; }  // Scheduled departure time
    public string SpecialRequest { get; private set; }  // Any special boarding gate requests
    public string BoardingGateName { get; set; } = null!;  // The name of the assigned gate
    public string Status { get; set; }  // Current flight status (e.g., "On Time", "Delayed")

    // Constructor to initialize the flight with its attributes
    public Flight(string flightNumber, string airlineCode, string origin, string destination, string expectedDeparture, string specialRequest)
    {
        FlightNumber = flightNumber;  // Assign flight number
        AirlineCode = airlineCode;  // Assign airline code
        Origin = origin;  // Assign origin airport
        Destination = destination;  // Assign destination airport
        ExpectedDeparture = expectedDeparture;  // Assign departure time
        SpecialRequest = specialRequest;  // Assign special request code
        BoardingGateName = null;  // No gate assigned initially
        Status = "On Time";  // Default status set to "On Time"
    }
}
