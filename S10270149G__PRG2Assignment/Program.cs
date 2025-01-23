using System;

class Program
{
    static void Main(string[] args)
    {
        // Create a new terminal instance
        Terminal terminal = new Terminal("Changi T5");

        // Add sample boarding gates
        terminal.AddBoardingGate(new BoardingGate("A1", true, false, true));  // Gate supports CFFT, LWTT
        terminal.AddBoardingGate(new BoardingGate("B2", false, true, false)); // Gate supports DDJB
        terminal.AddBoardingGate(new BoardingGate("C3", true, true, false));  // Gate supports CFFT, DDJB

        // Add sample flights
        Flight flight1 = new Flight("SQ693", "SQ", "NRT", "SIN", "10:30 AM", "DDJB");  // Special request: DDJB
        Flight flight2 = new Flight("CX312", "CX", "SIN", "HKD", "1:00 PM", "CFFT");   // Special request: CFFT

        // Add flights to terminal
        terminal.AddFlight(flight1);
        terminal.AddFlight(flight2);

        // Interactive menu to demonstrate features
        while (true)
        {
            Console.WriteLine("\n--- Flight Information Display System (FIDS) ---");
            Console.WriteLine("1. List All Boarding Gates");
            Console.WriteLine("2. Assign Boarding Gate to Flight");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    terminal.ListAllBoardingGates();  // Feature 4
                    break;

                case "2":
                    terminal.AssignBoardingGateToFlight();  // Feature 5
                    break;

                case "3":
                    Console.WriteLine("Exiting program. Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
