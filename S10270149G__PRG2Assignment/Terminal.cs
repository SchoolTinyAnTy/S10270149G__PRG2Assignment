//==========================================================
// Student Number : S10270149G
// Partner Number : S10266900J
// Student Name   : Orlando Lee Ming Kai
// Partner Name   : Ewe Yoke Kay 
//==========================================================


namespace S10270149G__PRG2Assignment;
using System;
using System.Collections.Generic;
using System.IO;

// The Terminal class manages airlines, flights, and boarding gates.
class Terminal
{
    // Properties to store terminal-related information
    public string TerminalName { get; set; }  // Stores the terminal name
    public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();  // Stores airlines
    public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();  // Stores flights
    public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();  // Stores boarding gates

    // Constructor initializes the terminal with a given name.
    public Terminal(string name)
    {
        TerminalName = name; // Assigns the terminal name
    }

    // ✅ Method to find the airline associated with a given flight
    public Airline GetAirlineFromFlight(Flight flight)
    {
        if (flight == null) return null; // Prevents errors by returning null if flight is invalid

        string flightCode = flight.FlightNumber.Split(" ")[0]; // Extracts airline code from flight number
        if (Airlines.ContainsKey(flightCode)) // Checks if the airline exists in the dictionary
        {
            return Airlines[flightCode]; // Returns the corresponding airline
        }

        return null; // If no matching airline is found, return null
    }

    // ✅ Method to add a new airline to the dictionary
    public bool AddAirline(Airline airline)
    {
        if (!Airlines.ContainsKey(airline.Code)) // Check if airline code is unique
        {
            Airlines.Add(airline.Code, airline); // Adds airline to dictionary
            return true; // Returns true indicating successful addition
        }
        Console.WriteLine("Error: Airline already exists."); // Prints error if airline already exists
        return false; // Returns false if airline was not added
    }

    // ✅ Method to add a new flight to the dictionary
    public bool AddFlight(Flight flight)
    {
        if (!Flights.ContainsKey(flight.FlightNumber)) // Check if flight number is unique
        {
            Flights.Add(flight.FlightNumber, flight); // Adds flight to dictionary
            return true; // Returns true indicating successful addition
        }
        Console.WriteLine("Error: Flight already exists."); // Prints error if flight already exists
        return false; // Returns false if flight was not added
    }

    // ✅ Method to add a new boarding gate to the dictionary
    public bool AddBoardingGate(BoardingGate gate)
    {
        if (!BoardingGates.ContainsKey(gate.GateName)) // Check if gate is unique
        {
            BoardingGates.Add(gate.GateName, gate); // Adds boarding gate to dictionary
            return true; // Returns true indicating successful addition
        }
        Console.WriteLine("Error: Boarding gate already exists."); // Prints error if gate already exists
        return false; // Returns false if gate was not added
    }

    // ✅ Feature 4: List All Boarding Gates with Exact Formatting as Sample Output
    public void ListAllBoardingGates()
    {
        // Check if there are no boarding gates and display a message
        if (BoardingGates.Count == 0)
        {
            Console.WriteLine("No boarding gates found. Ensure data is loaded.");
            return; // Exit the method if there are no gates to display
        }

        // Prints a header section to match sample output
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");

        // Prints the column headers exactly as in the sample output
        Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20}",
                          "Gate Name", "DDJB", "CFFT", "LWTT");

        // Loops through each boarding gate stored in the dictionary
        foreach (var gate in BoardingGates.Values)
        {
            // Prints gate details in the required format
            Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20}",
                              gate.GateName,
                              gate.SupportsDDJB ? "True" : "False",
                              gate.SupportsCFFT ? "True" : "False",
                              gate.SupportsLWTT ? "True" : "False");
        }

        // Prints closing line for formatting consistency
        Console.WriteLine("=============================================");
    }
}
