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

// The Terminal class manages airlines, flights, boarding gates, and gate fees.
public class Terminal
{
    private string terminalName; // Terminal name
    private Dictionary<string, Airline> airlines = new Dictionary<string, Airline>(); // Dictionary to store airlines
    private Dictionary<string, Flight> flights = new Dictionary<string, Flight>(); // Dictionary to store flights
    private Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>(); // Dictionary to store boarding gates
    private Dictionary<string, double> gateFees = new Dictionary<string, double>(); // Dictionary to store gate fees

    // Properties for terminal-related information
    public string TerminalName { get; set; } // Terminal name
    public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>(); // Dictionary to store airlines
    public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>(); // Dictionary to store flights
    public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>(); // Dictionary to store boarding gates
    public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>(); // Dictionary to store gate fees

    // Constructor initializes the terminal with a given name.
    public Terminal(string name)
    {
        TerminalName = name; // Initialize terminal name
    }

    // Method to find the airline associated with a given flight
    public Airline GetAirlineFromFlight(Flight flight)
    {
        if (flight == null) return null; // Check for null flight
        string flightCode = flight.FlightNumber.Split(" ")[0]; // Get flight code
        return Airlines.ContainsKey(flightCode) ? Airlines[flightCode] : null; // Return airline if found
    }

    // Method to add a new airline
    public bool AddAirline(Airline airline)
    {
        if (airline == null) throw new ArgumentNullException(nameof(airline)); // Check for null airline
        if (!Airlines.ContainsKey(airline.Code)) // Check if airline already exists
        {
            Airlines.Add(airline.Code, airline); // Add airline to dictionary
            return true; // Return true if airline is added
        }
        Console.WriteLine("Error: Airline already exists."); // Print error message if airline exists
        return false; // Return false if airline is not added
    }

    // Method to add a new flight
    public bool AddFlight(Flight flight)
    {
        if (flight == null) throw new ArgumentNullException(nameof(flight)); // Check for null flight
        if (!Flights.ContainsKey(flight.FlightNumber)) // Check if flight already exists
        {
            Flights.Add(flight.FlightNumber, flight); // Add flight to dictionary
            return true; // Return true if flight is added
        }
        Console.WriteLine("Error: Flight already exists."); // Print error message if flight exists
        return false; // Return false if flight is not added
    }

    // Method to add a new boarding gate and assign default fees
    public bool AddBoardingGate(BoardingGate gate)
    {
        if (gate == null) throw new ArgumentNullException(nameof(gate)); // Check for null gate
        if (!BoardingGates.ContainsKey(gate.GateName)) // Check if gate already exists
        {
            BoardingGates.Add(gate.GateName, gate); // Add gate to dictionary

            // Assign default boarding gate fee
            double gateFee = 300; // Base fee
            if (gate.SupportsDDJB) gateFee += 300; // Additional fee for DDJB support
            if (gate.SupportsCFFT) gateFee += 150; // Additional fee for CFFT support
            if (gate.SupportsLWTT) gateFee += 500; // Additional fee for LWTT support

            GateFees[gate.GateName] = gateFee; // Store fee in dictionary

            return true; // Return true if gate is added
        }
        Console.WriteLine("Error: Boarding gate already exists."); // Print error message if gate exists
        return false; // Return false if gate is not added
    }

    // Method to calculate total fees collected from all boarding gates
    public double CalculateTotalGateFees()
    {
        double totalFees = 0; // Initialize total fees
        foreach (var gate in BoardingGates.Values) // Iterate through boarding gates
        {
            if (gate.AssignedFlight != null) // Check if gate has an assigned flight
            {
                totalFees += GateFees[gate.GateName]; // Add assigned gate fees
            }
        }
        return totalFees; // Return total fees
    }

    // Feature 4: List All Boarding Gates with Exact Formatting as Sample Output
    public void ListAllBoardingGates()
    {
        if (BoardingGates.Count == 0) // Check if there are no boarding gates
        {
            Console.WriteLine("No boarding gates found. Ensure data is loaded."); // Print message if no gates found
            return; // Exit method
        }

        Console.WriteLine("============================================="); // Print header
        Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5"); // Print header
        Console.WriteLine("============================================="); // Print header
        Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20}", // Print column headers
                          "Gate Name", "DDJB", "CFFT", "LWTT");

        foreach (var gate in BoardingGates.Values) // Iterate through boarding gates
        {
            Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20}", // Print gate details
                              gate.GateName,
                              gate.SupportsDDJB ? "True" : "False", // Print DDJB support
                              gate.SupportsCFFT ? "True" : "False", // Print CFFT support
                              gate.SupportsLWTT ? "True" : "False"); // Print LWTT support
        }

        Console.WriteLine("============================================="); // Print footer
    }
}
