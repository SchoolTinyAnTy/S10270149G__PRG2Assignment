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

        LoadAirlines(terminal);
        LoadBoardingGates(terminal);

        AddFlights(terminal);

        Console.WriteLine($"Loading Airlines...\n{terminal.Airlines.Count} Airlines Loaded!");
        Console.WriteLine($"Loading Boarding Gates...\n{terminal.BoardingGates.Count} Boarding Gates Loaded!");
        Console.WriteLine($"Loading Flights...\n{terminal.Flights.Count} Flights Loaded!\n");

        while (true)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Welcome to Changi Airport Terminal 5");
            Console.WriteLine("=============================================");

            Console.WriteLine("1. List All Flights");
            Console.WriteLine("2. List Boarding Gates");
            Console.WriteLine("3. Assign  a Boarding Gate to  a Flight");
            Console.WriteLine("4. Create Flight");
            Console.WriteLine("5. Display Airline Flights");
            Console.WriteLine("6. Modify Flight Details");
            Console.WriteLine("7. Display Flight Schedule");
            Console.WriteLine("0. Exit");

            Console.Write("\nPlease select your option: ");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ListAllFlights(terminal);
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




        void LoadAirlines(Terminal terminal)
        {
            using (StreamReader sr = new StreamReader("airlines.csv"))
            {
                string? s = sr.ReadLine();
                while ((s = sr.ReadLine()) != null)
                {
                    string[] airlineDetails = s.Split(',');
                    Airline airline = new Airline(airlineDetails[0], airlineDetails[1]);

                    terminal.AddAirline(airline);
                }
            }
        }

        void LoadBoardingGates(Terminal terminal)
        {
            using (StreamReader sr = new StreamReader("boardinggates.csv"))
            {
                string? s = sr.ReadLine();
                while ((s = sr.ReadLine()) != null)
                {
                    string[] boardingGateDetails = s.Split(',');
                    BoardingGate boardingGate = new BoardingGate(boardingGateDetails[0], Convert.ToBoolean(boardingGateDetails[1]), Convert.ToBoolean(boardingGateDetails[2]), Convert.ToBoolean(boardingGateDetails[3]));

                    terminal.AddBoardingGate(boardingGate);
                }
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

        void ListAllFlights(Terminal terminal)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Flights for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Flight Number", -15}{"Airline Name", -22}{"Origin", -22}{"Destination", -22}Expected Departure/Arrival Time");
            foreach (Flight flight in terminal.Flights.Values)
            {
                Console.WriteLine($"{flight.FlightNumber,-15}{terminal.GetAirlineFromFlight(flight).Name,-22}{flight.Origin,-22}{flight.Destination,-22}{flight.ExpectedTime.ToString("dd/M/yyyy hh:mm tt")}");
            }
        }
    }
}
