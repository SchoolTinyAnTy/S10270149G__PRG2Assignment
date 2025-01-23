using System;

class Program
{
    static void Main(string[] args)
    {
        Terminal terminal = new Terminal("Changi T5");  // Create a new terminal instance

        // Add sample boarding gates with special support configurations
        terminal.AddBoardingGate(new BoardingGate("A1", true, false, true));  // Supports CFFT and LWTT
        terminal.AddBoardingGate(new BoardingGate("B2", false, true, false));  // Supports DDJB
        terminal.AddBoardingGate(new BoardingGate("C3", true, true, false));  // Supports CFFT and DDJB

        // Add sample airlines to the terminal
        terminal.AddAirline(new Airline("SQ", "Singapore Airlines"));  // Add Singapore Airlines
        terminal.AddAirline(new Airline("CX", "Cathay Pacific"));  // Add Cathay Pacific

        // Add sample flights to the terminal
        Flight flight1 = new Flight("SQ693", "SQ", "NRT", "SIN", "10:30 AM", "DDJB");  // Flight requiring DDJB
        Flight flight2 = new Flight("CX312", "CX", "SIN", "HKD", "1:00 PM", "CFFT");  // Flight requiring CFFT

        // Add flights to the terminal
        terminal.AddFlight(flight1);
        terminal.AddFlight(flight2);

        // Menu system for interacting with the terminal
        while (true)
        {
            Console.WriteLine("\n--- Flight Information Display System (FIDS) ---");
            Console.WriteLine("1. List All Boarding Gates");
            Console.WriteLine("2. Assign Boarding Gate to Flight");
            Console.WriteLine("3. Print Airline Fees");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();  // Read user input for menu selection

            switch (choice)
            {
                case "1":
                    terminal.ListAllBoardingGates();  // Display all boarding gates
                    break;

                case "2":
                    terminal.AssignBoardingGateToFlight();  // Assign a gate to a flight
                    break;

                case "3":
                    terminal.PrintAirlineFees();  // Print all airline fees
                    break;

                case "4":
                    Console.WriteLine("Exiting program. Goodbye!");
                    return;  // Exit the program

                default:
                    Console.WriteLine("Invalid option. Please try again.");  // Handle invalid input
                    break;
            }
        }
    }
}
