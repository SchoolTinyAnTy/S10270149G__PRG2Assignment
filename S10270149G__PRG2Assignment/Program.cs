//==========================================================
// Student Number : S10270149G
// Partner Number : S10266900J
// Student Name   : Orlando Lee Ming Kai
// Partner Name   : Ewe Yoke Kay 
//==========================================================
using S10270149G__PRG2Assignment;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace S10270149G__PRG2Assignment;

class Program
{   
    static void Main(string[] args)
    {
        Terminal terminal = new Terminal("Changi T5");

        // Adding sample boarding gates
        terminal.AddBoardingGate(new BoardingGate("A1", true, false, true));
        terminal.AddBoardingGate(new BoardingGate("B2", false, true, false));

        // Adding sample airlines
        terminal.AddAirline(new Airline("SQ", "Singapore Airlines"));
        terminal.AddAirline(new Airline("CX", "Cathay Pacific"));

        AddFlights(terminal);

        Console.WriteLine($"Loading Flights...\n{terminal.Flights.Count} Flights Loaded!\n");

        while (true)
        {
            Console.WriteLine("1. List All Boarding Gates");
            Console.WriteLine("2. Assign Boarding Gate to Flight");
            Console.WriteLine("3. Print Airline Fees");
            Console.WriteLine("4. Exit");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    terminal.ListAllBoardingGates();
                    break;
                case "2":
                    terminal.AssignBoardingGateToFlight();
                    break;
                case "3":
                    terminal.PrintAirlineFees();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        //method to add flights to dictionary
        void AddFlights(Terminal terminal)
        {
            using (StreamReader sr = new StreamReader("flights.csv"))
            {
                string? data = sr.ReadLine();
                if (data != null)
                {
                    string[] heading = data.Split(",");
                }
                while ((data = sr.ReadLine()) != null)
                {
                    string[] flightList = data.Split(",");
                    if (flightList[4] == null)
                    {
                        Flight flight = new NORMFlight(flightList[0], flightList[1], flightList[2], Convert.ToDateTime(flightList[3]));
                        terminal.AddFlight(flight);
                    }
                    else if (flightList[4] == "CFFT")
                    {
                        Flight flight = new CFFTFlight(flightList[0], flightList[1], flightList[2], Convert.ToDateTime(flightList[3]));
                        terminal.AddFlight(flight);
                    }
                    else if (flightList[4] == "DDJB")
                    {
                        Flight flight = new DDJBFlight(flightList[0], flightList[1], flightList[2], Convert.ToDateTime(flightList[3]));
                        terminal.AddFlight(flight);
                    }
                    else
                    {
                        Flight flight = new LWTTFlight(flightList[0], flightList[1], flightList[2], Convert.ToDateTime(flightList[3]));
                        terminal.AddFlight(flight);
                    }
                }
            }
        }
    }
}
