//==========================================================
// Student Number : S10270149G
// Partner Number : S10266900J
// Student Name   : Orlando Lee Ming Kai
// Partner Name   : Ewe Yoke Kay 
//==========================================================

namespace S10270149G__PRG2Assignment;
using System;
using System.Collections.Generic;

// Terminal class to manage flights, airlines, and boarding gates
class Terminal
{
    public string TerminalName { get; set; }  // Stores the terminal name
    public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();  // Dictionary to store airlines
    public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();  // Dictionary to store flights
    public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();  // Dictionary to store boarding gates

    // Constructor to initialize terminal name
    public Terminal(string name)
    {
        TerminalName = name;
    }

    // Adds an airline to the terminal
    public bool AddAirline(Airline airline)
    {
        if (!Airlines.ContainsKey(airline.Code))  // Check if airline doesn't already exist
        {
            Airlines.Add(airline.Code, airline);  // Add airline to dictionary
            return true;
        }
        return false;
    }

    // Adds a flight to the terminal
    public bool AddFlight(Flight flight)
    {
        if (!Flights.ContainsKey(flight.FlightNumber))  // Check if flight doesn't already exist
        {
            Flights.Add(flight.FlightNumber, flight);  // Add flight to dictionary
            return true;
        }
        return false;
    }

    // Adds a boarding gate to the terminal
    public bool AddBoardingGate(BoardingGate gate)
    {
        if (!BoardingGates.ContainsKey(gate.GateName))  // Check if gate doesn't already exist
        {
            BoardingGates.Add(gate.GateName, gate);  // Add gate to dictionary
            return true;
        }
        return false;
    }

    public Airline GetAirlineFromFlight(Flight flight)
    {
        string flight_code = flight.FlightNumber.Split(" ")[0];
        Airline airline_name = null;
        foreach (KeyValuePair<string, Airline> kvp in Airlines)
        {
            if (kvp.Key == flight_code)
            {
                airline_name = kvp.Value;
            }
        }
        return airline_name;
    }

    // Lists all available boarding gates and their assigned flights
    public void ListAllBoardingGates()
    {
        foreach (var gate in BoardingGates.Values)  // Loop through each boarding gate
        {
            Console.WriteLine(gate.ToString());  // Print boarding gate details
        }
    }
  
}

