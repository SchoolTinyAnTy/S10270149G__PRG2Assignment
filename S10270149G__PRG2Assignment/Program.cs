//==========================================================
// Student Number : S10270149G
// Partner Number : S10266900J
// Student Name   : Orlando Lee Ming Kai
// Partner Name   : Ewe Yoke Kay 
//==========================================================
using System;
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

        // Adding sample flights using specific subclasses
        Flight flight1 = new DDJBFlight("SQ693", "SIN", "NRT", DateTime.Parse("10:30 AM"));
        Flight flight2 = new CFFTFlight("CX312", "SIN", "HKD", DateTime.Parse("1:00 PM"));

        terminal.AddFlight(flight1);
        terminal.AddFlight(flight2);

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
    }
}
