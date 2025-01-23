using S10270149G__PRG2Assignment;
using System;
using System.Collections.Generic;

public class Terminal
{
    private string terminalName; // Stores the name of the terminal
    private Dictionary<string, Flight> flights; // Stores flights with flight number as key
    private Dictionary<string, BoardingGate> boardingGates; // Stores boarding gates

    // Constructor to initialize Terminal with a name
    public Terminal(string name)
    {
        terminalName = name;
        flights = new Dictionary<string, Flight>();
        boardingGates = new Dictionary<string, BoardingGate>();
    }

    // Method to add a boarding gate to the terminal
    public bool AddBoardingGate(BoardingGate gate)
    {
        if (!boardingGates.ContainsKey(gate.GateName))
        {
            boardingGates.Add(gate.GateName, gate);
            return true;
        }
        return false;
    }

    // Method to add a flight to the terminal
    public bool AddFlight(Flight flight)
    {
        if (!flights.ContainsKey(flight.FlightNumber))
        {
            flights.Add(flight.FlightNumber, flight);
            return true;
        }
        return false;
    }

    // Feature 4: List all boarding gates with their details
    public void ListAllBoardingGates()
    {
        Console.WriteLine("\n--- List of Boarding Gates ---\n");

        foreach (var gate in boardingGates.Values)
        {
            Console.WriteLine(gate.ToString());
        }
    }

    // Feature 5: Assign a boarding gate to a flight
    public void AssignBoardingGateToFlight()
    {
        Console.Write("Enter Flight Number: ");
        string flightNumber = Console.ReadLine();

        if (!flights.ContainsKey(flightNumber))
        {
            Console.WriteLine("Error: Flight not found.");
            return;
        }

        Flight selectedFlight = flights[flightNumber];
        Console.WriteLine($"Flight found: {selectedFlight}");

        Console.Write("Enter Boarding Gate Name: ");
        string gateName = Console.ReadLine();

        if (!boardingGates.ContainsKey(gateName))
        {
            Console.WriteLine("Error: Boarding Gate not found.");
            return;
        }

        BoardingGate gate = boardingGates[gateName];

        if (gate.AssignedFlight != null)
        {
            Console.WriteLine("Error: This boarding gate is already assigned to another flight.");
            return;
        }

        gate.AssignedFlight = selectedFlight;
        selectedFlight.BoardingGateName = gateName;

        Console.WriteLine($"Success: Flight {selectedFlight.FlightNumber} assigned to Gate {gate.GateName}");

        Console.Write("Would you like to update the flight status? (Y/N): ");
        string response = Console.ReadLine().ToUpper();

        if (response == "Y")
        {
            Console.WriteLine("Select new status: 1. Delayed, 2. Boarding, 3. On Time");
            int option;
            if (int.TryParse(Console.ReadLine(), out option))
            {
                selectedFlight.Status = option switch
                {
                    1 => "Delayed",
                    2 => "Boarding",
                    _ => "On Time"
                };
            }
        }

        Console.WriteLine("Boarding gate assignment completed successfully.");
    }

    public override string ToString()
    {
        return $"Terminal: {terminalName}, Flights: {flights.Count}, Gates: {boardingGates.Count}";
    }
}
