using S10270149G__PRG2Assignment;
using System;
using System.Collections.Generic;

public class Terminal
{
    private string terminalName;  // Stores the name of the terminal
    private Dictionary<string, Airline> airlines;  // Stores airlines with their codes
    private Dictionary<string, Flight> flights;  // Stores flights with their flight numbers
    private Dictionary<string, BoardingGate> boardingGates;  // Stores boarding gates with gate names
    private Dictionary<string, double> gateFees;  // Stores gate fees by airline code

    // Constructor to initialize the terminal with a name
    public Terminal(string name)
    {
        terminalName = name;
        airlines = new Dictionary<string, Airline>();  // Initialize airlines dictionary
        flights = new Dictionary<string, Flight>();  // Initialize flights dictionary
        boardingGates = new Dictionary<string, BoardingGate>();  // Initialize gates dictionary
        gateFees = new Dictionary<string, double>();  // Initialize gate fees dictionary
    }

    // Adds an airline to the terminal
    public bool AddAirline(Airline airline)
    {
        if (!airlines.ContainsKey(airline.Code))  // Check if airline does not already exist
        {
            airlines.Add(airline.Code, airline);  // Add airline to dictionary
            return true;
        }
        return false;
    }

    // Adds a boarding gate to the terminal
    public bool AddBoardingGate(BoardingGate gate)
    {
        if (!boardingGates.ContainsKey(gate.GateName))  // Check if gate is not already added
        {
            boardingGates.Add(gate.GateName, gate);  // Add gate to dictionary
            return true;
        }
        return false;
    }

    // Adds a flight to the terminal
    public bool AddFlight(Flight flight)
    {
        if (!flights.ContainsKey(flight.FlightNumber))  // Check if flight is not already added
        {
            flights.Add(flight.FlightNumber, flight);  // Add flight to dictionary
            return true;
        }
        return false;
    }

    // Retrieves the airline associated with a given flight
    public Airline GetAirlineFromFlight(Flight flight)
    {
        if (airlines.ContainsKey(flight.AirlineCode))  // Check if airline exists in dictionary
        {
            return airlines[flight.AirlineCode];  // Return the corresponding airline
        }
        return null;  // Return null if airline not found
    }

    // Lists all boarding gates with their details
    public void ListAllBoardingGates()
    {
        Console.WriteLine("\n--- List of Boarding Gates ---\n");
        foreach (var gate in boardingGates.Values)  // Loop through each gate in the dictionary
        {
            Console.WriteLine(gate.ToString());  // Print gate details
        }
    }

    // Assigns a boarding gate to a flight
    public void AssignBoardingGateToFlight()
    {
        Console.Write("Enter Flight Number: ");
        string flightNumber = Console.ReadLine();  // Read flight number from user

        if (!flights.ContainsKey(flightNumber))  // Check if flight exists
        {
            Console.WriteLine("Error: Flight not found.");
            return;
        }

        Flight selectedFlight = flights[flightNumber];  // Retrieve the flight object
        Console.WriteLine($"Flight found: {selectedFlight}");

        Console.Write("Enter Boarding Gate Name: ");
        string gateName = Console.ReadLine();  // Read gate name from user

        if (!boardingGates.ContainsKey(gateName))  // Check if gate exists
        {
            Console.WriteLine("Error: Boarding Gate not found.");
            return;
        }

        BoardingGate gate = boardingGates[gateName];  // Retrieve the boarding gate object

        if (gate.AssignedFlight != null)  // Check if the gate is already occupied
        {
            Console.WriteLine("Error: This boarding gate is already assigned to another flight.");
            return;
        }

        gate.AssignedFlight = selectedFlight;  // Assign flight to the gate
        selectedFlight.BoardingGateName = gateName;  // Update flight's assigned gate

        Console.WriteLine($"Success: Flight {selectedFlight.FlightNumber} assigned to Gate {gate.GateName}");

        Console.Write("Would you like to update the flight status? (Y/N): ");
        string response = Console.ReadLine().ToUpper();  // Read user input for status update

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

    // Prints the airline fees for assigned flights
    public void PrintAirlineFees()
    {
        foreach (var flight in flights.Values)  // Loop through flights
        {
            if (boardingGates.ContainsKey(flight.BoardingGateName))  // Check if gate exists for the flight
            {
                double fee = boardingGates[flight.BoardingGateName].CalculateFees();  // Calculate fees
                Console.WriteLine($"Airline: {flight.AirlineCode}, Gate: {flight.BoardingGateName}, Fee: {fee:C}");
            }
        }
    }

    // Converts terminal information to a string
    public override string ToString()
    {
        return $"Terminal: {terminalName}, Airlines: {airlines.Count}, Flights: {flights.Count}, Gates: {boardingGates.Count}";
    }
}
