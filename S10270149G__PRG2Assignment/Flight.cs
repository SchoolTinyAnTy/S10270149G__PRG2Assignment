abstract class Flight
{
    public string FlightNumber { get; set; }  // Unique identifier for the flight
    public string Origin { get; set; }  // Origin airport of the flight
    public string Destination { get; set; }  // Destination airport of the flight
    public DateTime ExpectedTime { get; set; }  // Scheduled departure time
    public string Status { get; set; }  // Current flight status (e.g., "On Time", "Delayed")

    // Constructor to initialize the flight with its attributes
    public Flight(string flightNumber, string origin, string destination, DateTime expectedTime)
    {
        FlightNumber = flightNumber;  // Assign flight number
        Origin = origin;  // Assign origin airport
        Destination = destination;  // Assign destination airport
        ExpectedTime = expectedTime;  // Assign departure time
        Status = "Scheduled";  // Default status set to "Scheduled"
    }

    public override string ToString()
    {
        return "Flight number:" + FlightNumber + "\tOrigin:" + Origin + "\tDestination:" + Destination + "\tExpected time:" + ExpectedTime + "\t" + Status;
    }
}
