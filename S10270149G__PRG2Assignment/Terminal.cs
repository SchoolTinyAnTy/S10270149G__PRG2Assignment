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

    // Assigns a boarding gate to a flight with input validation
    public void AssignBoardingGateToFlight()
    {
        string flightNumber;
        do
        {
            Console.Write("Enter Flight Number: ");
            flightNumber = Console.ReadLine()?.Trim();  // Read and trim input

            if (string.IsNullOrEmpty(flightNumber) || !Flights.ContainsKey(flightNumber))
            {
                Console.WriteLine("Invalid flight number. Please try again.");
            }
        } while (string.IsNullOrEmpty(flightNumber) || !Flights.ContainsKey(flightNumber));

        Flight selectedFlight = Flights[flightNumber];  // Retrieve the flight object
        Console.WriteLine($"Flight found: {selectedFlight}");

        string gateName;
        do
        {
            Console.Write("Enter Boarding Gate Name: ");
            gateName = Console.ReadLine()?.Trim();  // Read and trim input

            if (string.IsNullOrEmpty(gateName) || !BoardingGates.ContainsKey(gateName))
            {
                Console.WriteLine("Invalid gate name. Please enter a valid boarding gate.");
            }
        } while (string.IsNullOrEmpty(gateName) || !BoardingGates.ContainsKey(gateName));

        BoardingGate gate = BoardingGates[gateName];  // Retrieve the boarding gate object

        if (gate.AssignedFlight != null)  // Check if the gate is already occupied
        {
            Console.WriteLine("Error: This boarding gate is already assigned to another flight.");
            return;
        }

        gate.AssignedFlight = selectedFlight;  // Assign flight to the gate
        selectedFlight.Status = "On Time";  // Set flight status to 'On Time'
        selectedFlight.BoardingGateName = gateName;  // Assign gate name to flight

        Console.WriteLine($"Success: Flight {selectedFlight.FlightNumber} assigned to Gate {gate.GateName}");

        string updateStatus;
        do
        {
            Console.Write("Would you like to update the flight status? (Y/N): ");
            updateStatus = Console.ReadLine()?.Trim().ToUpper();

            if (updateStatus == "Y")
            {
                Console.WriteLine("Select new status: 1. Delayed, 2. Boarding, 3. On Time");
                int option;
                while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 3)
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                }

                selectedFlight.Status = option switch
                {
                    1 => "Delayed",
                    2 => "Boarding",
                    _ => "On Time"
                };

                Console.WriteLine($"Flight status updated to: {selectedFlight.Status}");
            }
        } while (updateStatus != "Y" && updateStatus != "N");

        Console.WriteLine("Boarding gate assignment completed successfully.");
    }

    // Prints all airline fees for the day
    public void PrintAirlineFees()
    {
        foreach (var airline in Airlines.Values)  // Loop through each airline
        {
            Console.WriteLine(airline.ToString());  // Print airline details
        }
    }
}

