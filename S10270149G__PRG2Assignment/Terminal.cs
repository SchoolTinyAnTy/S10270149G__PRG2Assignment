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

// ✅ The Terminal class manages airlines, flights, boarding gates, and gate fees.
public class Terminal
{ 
    private string _terminalName;
    private Dictionary<string, Airline> _airlines = new Dictionary<string, Airline>();
    private Dictionary<string, Flight> _flights = new Dictionary<string, Flight>();
    private Dictionary<string, BoardingGate> _boardingGates = new Dictionary<string, BoardingGate>();
    private Dictionary<string, double> _gateFees = new Dictionary<string, double>();

    // ✅ Properties for terminal-related information
    public string TerminalName { get; set; }
    public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
    public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
    public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();

    // ✅ Stores gate fees using gate names as keys
    public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();

    // ✅ Constructor initializes the terminal with a given name.
    public Terminal(string name)
    {
        TerminalName = name;
    }

    // ✅ Method to find the airline associated with a given flight
    public Airline GetAirlineFromFlight(Flight flight)
    {
        if (flight == null) return null;
        string flightCode = flight.FlightNumber.Split(" ")[0];
        return Airlines.ContainsKey(flightCode) ? Airlines[flightCode] : null;
    }

    // ✅ Method to add a new airline
    public bool AddAirline(Airline airline)
    {
        if (!Airlines.ContainsKey(airline.Code))
        {
            Airlines.Add(airline.Code, airline);
            return true;
        }
        Console.WriteLine("Error: Airline already exists.");
        return false;
    }

    // ✅ Method to add a new flight
    public bool AddFlight(Flight flight)
    {
        if (!Flights.ContainsKey(flight.FlightNumber))
        {
            Flights.Add(flight.FlightNumber, flight);
            return true;
        }
        Console.WriteLine("Error: Flight already exists.");
        return false;
    }

    // ✅ Method to add a new boarding gate and assign default fees
    public bool AddBoardingGate(BoardingGate gate)
    {
        if (!BoardingGates.ContainsKey(gate.GateName))
        {
            BoardingGates.Add(gate.GateName, gate);

            // ✅ Assign default boarding gate fee
            double gateFee = 300;
            if (gate.SupportsDDJB) gateFee += 300;
            if (gate.SupportsCFFT) gateFee += 150;
            if (gate.SupportsLWTT) gateFee += 500;

            GateFees[gate.GateName] = gateFee;  // ✅ Store fee in dictionary

            return true;
        }
        Console.WriteLine("Error: Boarding gate already exists.");
        return false;
    }

    // ✅ Method to calculate total fees collected from all boarding gates
    public double CalculateTotalGateFees()
    {
        double totalFees = 0;
        foreach (var gate in BoardingGates.Values)
        {
            if (gate.AssignedFlight != null)
            {
                totalFees += GateFees[gate.GateName]; // ✅ Add assigned gate fees
            }
        }
        return totalFees;
    }

    // ✅ Feature 4: List All Boarding Gates with Exact Formatting as Sample Output
    public void ListAllBoardingGates()
    {
        if (BoardingGates.Count == 0)
        {
            Console.WriteLine("No boarding gates found. Ensure data is loaded.");
            return;
        }

        Console.WriteLine("=============================================");
        Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20} {4,-10}",
                          "Gate Name", "DDJB", "CFFT", "LWTT", "Gate Fee ($)");

        foreach (var gate in BoardingGates.Values)
        {
            Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20} {4,-10}",
                              gate.GateName,
                              gate.SupportsDDJB ? "True" : "False",
                              gate.SupportsCFFT ? "True" : "False",
                              gate.SupportsLWTT ? "True" : "False",
                              GateFees[gate.GateName]);
        }

        Console.WriteLine("=============================================");
    }
}
