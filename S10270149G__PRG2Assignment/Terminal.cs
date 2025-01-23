using S10270149G__PRG2Assignment;
using System;
using System.Collections.Generic;

public class Terminal
{
    // Attributes
    private string terminalName;
    private Dictionary<string, Airline> airlines;
    private Dictionary<string, Flight> flights;
    private Dictionary<string, BoardingGate> boardingGates;
    private Dictionary<string, double> gateFees;

    // Constructor
    public Terminal(string name)
    {
        terminalName = name;
        airlines = new Dictionary<string, Airline>();
        flights = new Dictionary<string, Flight>();
        boardingGates = new Dictionary<string, BoardingGate>();
        gateFees = new Dictionary<string, double>();
    }

    // Method to add an airline
    public bool AddAirline(Airline airline)
    {
        if (!airlines.ContainsKey(airline.Code))
        {
            airlines.Add(airline.Code, airline);
            return true;
        }
        return false;
    }

    // Method to add a boarding gate
    public bool AddBoardingGate(BoardingGate gate)
    {
        if (!boardingGates.ContainsKey(gate.GateName))
        {
            boardingGates.Add(gate.GateName, gate);
            return true;
        }
        return false;
    }

    // Method to get an airline from a flight
    public Airline GetAirlineFromFlight(Flight flight)
    {
        if (airlines.ContainsKey(flight.AirlineCode))
        {
            return airlines[flight.AirlineCode];
        }
        return null;
    }

    // Method to print airline fees
    public void PrintAirlineFees()
    {
        foreach (var fee in gateFees)
        {
            Console.WriteLine($"Airline: {fee.Key}, Fee: {fee.Value:C}");
        }
    }

    // Overriding ToString method
    public override string ToString()
    {
        return $"Terminal Name: {terminalName}, Airlines Count: {airlines.Count}, Flights Count: {flights.Count}, Boarding Gates Count: {boardingGates.Count}";
    }
}
