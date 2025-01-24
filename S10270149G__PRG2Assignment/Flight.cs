﻿//==========================================================
// Student Number : S10270149G
// Partner Number : S10266900J
// Student Name   : Orlando Lee Ming Kai
// Partner Name   : Ewe Yoke Kay 
//==========================================================
namespace S10270149G__PRG2Assignment;

// Abstract class representing a generic flight
abstract class Flight
{
    public string FlightNumber { get; set; }  // Unique identifier for the flight
    public string Origin { get; set; }  // Origin airport of the flight
    public string Destination { get; set; }  // Destination airport of the flight
    public DateTime ExpectedTime { get; set; }  // Scheduled departure time
    public string Status { get; set; }  // Current flight status

    // Constructor to initialize a flight object
    public Flight(string flightNumber, string origin, string destination, DateTime expectedTime)
    {
        FlightNumber = flightNumber;
        Origin = origin;
        Destination = destination;
        ExpectedTime = expectedTime;
        Status = "Scheduled";
    }

    // Returns flight details as a formatted string
    public override string ToString()
    {
        return $"Flight {FlightNumber} from {Origin} to {Destination}, Departure: {ExpectedTime}, Status: {Status}";
    }
}
