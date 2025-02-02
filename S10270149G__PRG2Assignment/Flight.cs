//==========================================================
// Student Number : S10270149G
// Partner Number : S10266900J
// Student Name   : Orlando Lee Ming Kai
// Partner Name   : Ewe Yoke Kay 
//==========================================================
namespace S10270149G__PRG2Assignment;
using System;

// The Flight class represents a flight with key details.
public class Flight : IComparable<Flight>
{
    private string flightNumber;  // Unique flight identifier
    private string origin;  // Departure location
    private string destination;  // Arrival location
    private DateTime expectedTime;  // Scheduled departure/arrival time
    private string status;  // Flight status

    public string FlightNumber { get; set; }  // Unique flight identifier
    public string Origin { get; set; }  // Departure location
    public string Destination { get; set; }  // Arrival location
    public DateTime ExpectedTime { get; set; }  // Scheduled departure/arrival time
    public string Status { get; set; }  // Flight status

    // Constructor initializes a flight with validation to prevent null values.
    public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "Scheduled")
    {
        FlightNumber = flightNumber ?? throw new ArgumentNullException(nameof(flightNumber)); // Check for null flight number
        Origin = origin ?? throw new ArgumentNullException(nameof(origin)); // Check for null origin
        Destination = destination ?? throw new ArgumentNullException(nameof(destination)); // Check for null destination
        ExpectedTime = expectedTime;
        Status = status;
    }

    public double CalculateFees()
    {
        double baseFee = 300;
        if (Destination == "Singapore (SIN)")
        {
            baseFee += 500;
        }
        else if (Origin == "Singapore (SIN)")
        {
            baseFee += 800;
        }
        return baseFee;
    }

    // CompareTo method implementation
    public int CompareTo(Flight? other)
    {
        if (other == null) return 1; // Check for null other flight
        return ExpectedTime.CompareTo(other.ExpectedTime); // Compare expected times
    }

    // Converts flight information into a readable string.
    public override string ToString()
    {
        return $"Flight: {FlightNumber}, Origin: {Origin}, Destination: {Destination}, Expected Time: {ExpectedTime}, Status: {Status}";// Return formatted string with flight details
    }
}
