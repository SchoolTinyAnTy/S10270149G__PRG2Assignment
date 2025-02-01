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
    public string FlightNumber { get; private set; }  // Unique flight identifier
    public string Origin { get; set; }  // Departure location
    public string Destination { get; set; }  // Arrival location
    public DateTime ExpectedTime { get; set; }  // Scheduled departure/arrival time
    public string Status { get; set; }  // ✅ Fix: Added missing Status property

    // Constructor initializes a flight with validation to prevent null values.
    public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "Scheduled")
    {
        FlightNumber = flightNumber ?? "Unknown";  // Prevents null flight number
        Origin = origin ?? "Unknown";  // Prevents null origin
        Destination = destination ?? "Unknown";  // Prevents null destination
        ExpectedTime = expectedTime;  // Ensures a valid DateTime is assigned
        Status = status ?? "Scheduled";  // ✅ Assigns default status if none is provided
    }

    // ✅ Fix: Corrected CompareTo method implementation
    public int CompareTo(Flight? other)
    {
        if (other == null) return 1; // Ensures null-safe comparison
        return ExpectedTime.CompareTo(other.ExpectedTime); // Sorts flights by expected time
    }

    // Converts flight information into a readable string.
    public override string ToString()
    {
        return $"{FlightNumber}: {Origin} -> {Destination} at {ExpectedTime} (Status: {Status})";
    }
}