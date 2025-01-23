using S10270149G__PRG2Assignment;
using System;
using System.Collections.Generic;

public class Terminal
{
    private string terminalName; // Stores the name of the terminal
    private Dictionary<string, Airline> airlines; // Stores airlines with their code as key
    private Dictionary<string, Flight> flights; // Stores flights with their flight number as key
    private Dictionary<string, BoardingGate> boardingGates; // Stores boarding gates with gate name as key

    // Constructor to initialize Terminal with a name
    public Terminal(string name)
    {
        terminalName = name;
        airlines = new Dictionary<string, Airline>(); // Initialize airlines dictionary
        flights = new Dictionary<string, Flight>(); // Initialize flights dictionary
        boardingGates = new Dictionary<string, BoardingGate>(); // Initialize boarding gates dictionary
    }

    // Adds an airline to the terminal
    public bool AddAirline(Airline airline)
    {
        if (!airlines.ContainsKey(airline.Code)) // Check if airline does not already exist
        {
            airlines.Add(airline.Code, airline); // Add airline to dictionary
            return true;
        }
        return false; // Return false if airline already exists
    }

    // Adds a boarding gate to the terminal
    public bool AddBoardingGate(BoardingGate gate)
    {
        if (!boardingGates.ContainsKey(gate.GateName)) // Check if gate is not already added
        {
            boardingGates.Add(gate.GateName, gate); // Add gate to dictionary
            return true;
        }
        return false; // Return false if gate already exists
    }

    // Retrieves the airline associated with a given flight
    public Airline GetAirlineFromFlight(Flight flight)
    {
        if (airlines.ContainsKey(flight.AirlineCode)) // Check if airline exists
        {
            return airlines[flight.AirlineCode]; // Return the airline object
        }
        return null; // Return null if airline not found
    }

    // Lists all boarding gates and their details
    public void ListAllBoardingGates()
    {
        Console.WriteLine("Listing all boarding gates:\n");

        foreach (var gate in boardingGates.Values) // Loop through each boarding gate
        {
            Console.WriteLine(gate.ToString()); // Print gate details
        }
    }

    // Assigns a boarding gate to a flight
    public void AssignBoardingGateToFlight()
    {
        Console.Write("Enter Flight Number: ");
        string flightNumber = Console.ReadLine(); // Read flight number from user

        if (!flights.ContainsKey(flightNumber)) // Check if flight exists
        {
            Console.WriteLine("Error: Flight not found.");
            return;
        }

        Flight selectedFlight = flights[flightNumber]; // Retrieve the flight
        Console.WriteLine($"Flight found: {selectedFlight}");

        Console.Write("Enter Boarding Gate Name: ");
        string gateName = Console.ReadLine(); // Read gate name from user

        if (!boardingGates.ContainsKey(gateName)) // Check if gate exists
        {
            Console.WriteLine("Error: Boarding Gate not found.");
            return;
        }

        BoardingGate gate = boardingGates[gateName]; // Retrieve the gate

        if (gate.AssignedFlight != null) // Check if gate is already assigned
        {
            Console.WriteLine("Error: This boarding gate is already assigned.");
            return;
        }

        gate.AssignedFlight = selectedFlight; // Assign flight to gate
        selectedFlight.BoardingGateName = gateName; // Update flight's boarding gate

        Console.WriteLine($"Success: Flight {selectedFlight.FlightNumber} assigned to Gate {gate.GateName}");

        // Prompt user to update the flight status
        Console.Write("Would you like to update the flight status? (Y/N): ");
        string response = Console.ReadLine().ToUpper();

        if (response == "Y")
        {
            Console.WriteLine("Select new status: 1. Delayed, 2. Boarding, 3. On Time");
            int option = int.Parse(Console.ReadLine()); // Read user input for status update
            selectedFlight.Status = option switch
            {
                1 => "Delayed",
                2 => "Boarding",
                _ => "On Time"
            };
        }

        Console.WriteLine("Boarding gate assignment completed successfully.");
    }

    // Overrides the ToString method to display terminal information
    public override string ToString()
    {
        return $"Terminal: {terminalName}, Airlines: {airlines.Count}, Flights: {flights.Count}, Gates: {boardingGates.Count}";
    }
}
