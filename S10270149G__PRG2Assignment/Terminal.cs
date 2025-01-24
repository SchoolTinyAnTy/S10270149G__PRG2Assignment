//==========================================================
// Student Number : S10270149G
// Partner Number : S10266900J
// Student Name   : Orlando Lee Ming Kai
// Partner Name   : Ewe Yoke Kay 
//==========================================================
namespace S10270149G__PRG2Assignment;
using System;
using System.Collections.Generic;

class Terminal
{
    public string TerminalName { get; set; }
    public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
    public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
    public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();

    public Terminal(string name)
    {
        TerminalName = name;
    }

    public bool AddAirline(Airline airline)
    {
        if (!Airlines.ContainsKey(airline.Code))
        {
            Airlines.Add(airline.Code, airline);
            return true;
        }
        return false;
    }

    public bool AddFlight(Flight flight)
    {
        if (!Flights.ContainsKey(flight.FlightNumber))
        {
            Flights.Add(flight.FlightNumber, flight);
            return true;
        }
        return false;
    }

    public bool AddBoardingGate(BoardingGate gate)
    {
        if (!BoardingGates.ContainsKey(gate.GateName))
        {
            BoardingGates.Add(gate.GateName, gate);
            return true;
        }
        return false;
    }

    public void ListAllBoardingGates()
    {
        foreach (var gate in BoardingGates.Values)
        {
            Console.WriteLine(gate.ToString());
        }
    }

    public void AssignBoardingGateToFlight()
    {
        Console.Write("Enter Flight Number: ");
        string flightNumber = Console.ReadLine();
        if (!Flights.ContainsKey(flightNumber))
        {
            Console.WriteLine("Flight not found.");
            return;
        }

        Console.Write("Enter Boarding Gate Name: ");
        string gateName = Console.ReadLine();
        if (!BoardingGates.ContainsKey(gateName))
        {
            Console.WriteLine("Gate not found.");
            return;
        }

        BoardingGates[gateName].AssignedFlight = Flights[flightNumber];
        Console.WriteLine($"Flight {flightNumber} assigned to Gate {gateName}");
    }

    public void PrintAirlineFees()
    {
        foreach (var airline in Airlines.Values)
        {
            Console.WriteLine(airline.ToString());
        }
    }
}
