using S10270149G__PRG2Assignment;
using System;
using System.Collections.Generic;

class Terminal
{
    public string TerminalName { get; set; }  // Stores the name of the terminal
    public Dictionary<string, Airline> Airlines { get; set; }  // Stores airlines with their codes
    public Dictionary<string, Flight> Flights { get; set; }  // Stores flights with their flight numbers
    public Dictionary<string, BoardingGate> BoardingGates { get; set; }  // Stores boarding gates with gate names
    public Dictionary<string, double> GateFees { get; set; }  // Stores gate fees by airline code

    // Constructor to initialize the terminal with a name
    public Terminal(string name)
    {
        TerminalName = name;
        Airlines = new Dictionary<string, Airline>();  // Initialize airlines dictionary
        Flights = new Dictionary<string, Flight>();  // Initialize flights dictionary
        BoardingGates = new Dictionary<string, BoardingGate>();  // Initialize gates dictionary
        GateFees = new Dictionary<string, double>();  // Initialize gate fees dictionary
    }

    // Adds an airline to the terminal
    public bool AddAirline(Airline airline)
    {
        if (!Airlines.ContainsKey(airline.Code))  // Check if airline does not already exist
        {
            Airlines.Add(airline.Code, airline);  // Add airline to dictionary
            return true;
        }
        return false;
    }

    // Adds a boarding gate to the terminal
    public bool AddBoardingGate(BoardingGate gate)
    {
        if (!BoardingGates.ContainsKey(gate.GateName))  // Check if gate is not already added
        {
            BoardingGates.Add(gate.GateName, gate);  // Add gate to dictionary
            return true;
        }
        return false;
    }

    // Adds a flight to the terminal
    public bool AddFlight(Flight flight)
    {
        if (!Flights.ContainsKey(flight.FlightNumber))  // Check if flight is not already added
        {
            Flights.Add(flight.FlightNumber, flight);  // Add flight to dictionary
            return true;
        }
        return false;
    }

    // Retrieves the airline associated with a given flight
    public Airline GetAirlineFromFlight(Flight flight)
    {
        string airlineCode = flight.FlightNumber.Split(' ')[0];  // Retrieve code from FlightNumber
        return Airlines[airlineCode];  // Returns Airline object using code
    }

    // Lists all boarding gates with their details
    public void ListAllBoardingGates()
    {
        Console.WriteLine("\n--- List of Boarding Gates ---\n");
        foreach (BoardingGate gate in BoardingGates.Values)  // Loop through each gate in the dictionary
        {
            Console.WriteLine(gate.ToString());  // Print gate details
        }
    }

    // Assigns a boarding gate to a flight
    public void AssignBoardingGateToFlight()
    {
        Console.Write("Enter Flight Number: ");
        string flightNumber = Console.ReadLine();  // Read flight number from user

        if (!Flights.ContainsKey(flightNumber))  // Check if flight exists
        {
            Console.WriteLine("Error: Flight not found.");
            return;
        }

        Flight selectedFlight = Flights[flightNumber];  // Retrieve the flight object
        Console.WriteLine($"Flight found: {selectedFlight}");

        Console.Write("Enter Boarding Gate Name: ");
        string gateName = Console.ReadLine();  // Read gate name from user

        if (!BoardingGates.ContainsKey(gateName))  // Check if gate exists
        {
            Console.WriteLine("Error: Boarding Gate not found.");
            return;
        }

        BoardingGate gate = BoardingGates[gateName];  // Retrieve the boarding gate object

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
        foreach (var flight in Flights.Values)  // Loop through flights
        {
            if (BoardingGates.ContainsKey(flight.BoardingGateName))  // Check if gate exists for the flight
            {
                double fee = BoardingGates[flight.BoardingGateName].CalculateFees();  // Calculate fees
                Console.WriteLine($"Airline: {flight.AirlineCode}, Gate: {flight.BoardingGateName}, Fee: {fee:C}");
            }
        }
    }

    // Converts terminal information to a string
    public override string ToString()
    {
        return $"Terminal: {TerminalName}, Airlines: {Airlines.Count}, Flights: {Flights.Count}, Gates: {BoardingGates.Count}";
    }
}
