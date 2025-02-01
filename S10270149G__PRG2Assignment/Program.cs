//==========================================================
// Student Number : S10270149G
// Partner Number : S10266900J
// Student Name   : Orlando Lee Ming Kai
// Partner Name   : Ewe Yoke Kay 
//==========================================================
using S10270149G__PRG2Assignment;
using System;
using System.Numerics;
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
            Console.WriteLine("3. Assign a Boarding Gate to a Flight");
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
                    terminal.ListAllBoardingGates(); 
                    break;
                case "3":
                    AssignBoardingGate(terminal);
                    break;
                case "4":
                    CreateNewFlight(terminal);
                    break;
                case "7":
                    DisplayFlightSchedule(terminal);
                    break;
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

        // ✅ Feature 3: Assign a Boarding Gate to a Flight (Matches Sample Output)
        static void AssignBoardingGate(Terminal terminal)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Assign a Boarding Gate to a Flight");
            Console.WriteLine("=============================================");

            bool validFlight = false;
            bool validGate = false;
            string flightNum = "";
            string gateName = "";

            // ✅ Step 1: Prompt user for a valid flight number
            while (!validFlight)
            {
                Console.Write("Enter Flight Number: ");
                flightNum = Console.ReadLine()?.ToUpper();

                if (!terminal.Flights.ContainsKey(flightNum))
                {
                    Console.WriteLine("Error: Flight number not found. Please enter a valid flight number.");
                }
                else
                {
                    validFlight = true;
                }
            }

            // ✅ Step 2: Prompt user for a valid boarding gate name
            while (!validGate)
            {
                Console.Write("Enter Boarding Gate Name: ");
                gateName = Console.ReadLine()?.ToUpper();

                if (!terminal.BoardingGates.ContainsKey(gateName))
                {
                    Console.WriteLine("Error: Boarding gate not found. Please enter a valid gate name.");
                }
                else if (terminal.BoardingGates[gateName].AssignedFlight != null)
                {
                    Console.WriteLine("Error: This boarding gate is already assigned to another flight.");
                }
                else
                {
                    validGate = true;
                }
            }

            // ✅ Step 3: Retrieve flight and gate objects
            Flight flight = terminal.Flights[flightNum];
            BoardingGate selectedGate = terminal.BoardingGates[gateName];

            // ✅ Step 4: Assign the flight to the boarding gate
            selectedGate.AssignedFlight = flight;

            // ✅ Step 5: Display flight and gate details (Matches Sample Output)
            Console.WriteLine("\n=============================================");
            Console.WriteLine($"Flight Number: {flight.FlightNumber}");
            Console.WriteLine($"Origin: {flight.Origin}");
            Console.WriteLine($"Destination: {flight.Destination}");
            Console.WriteLine($"Expected Time: {flight.ExpectedTime:dd/M/yyyy hh:mm:ss tt}");

            // Determine the special request code
            string specialRequest = flight is NORMFlight ? "None" :
                                    flight is CFFTFlight ? "CFFT" :
                                    flight is DDJBFlight ? "DDJB" :
                                    "LWTT";
            Console.WriteLine($"Special Request Code: {specialRequest}");

            Console.WriteLine($"Boarding Gate Name: {gateName}");
            Console.WriteLine($"Supports DDJB: {selectedGate.SupportsDDJB}");
            Console.WriteLine($"Supports CFFT: {selectedGate.SupportsCFFT}");
            Console.WriteLine($"Supports LWTT: {selectedGate.SupportsLWTT}");
            Console.WriteLine("=============================================\n");

            // ✅ Step 6: Ask if the user wants to update the flight status
            bool statusUpdated = false;
            while (!statusUpdated)
            {
                Console.Write("Would you like to update the status of the flight? (Y/N): ");
                string? updateStatus = Console.ReadLine()?.Trim().ToUpper();

                if (updateStatus == "Y")
                {
                    Console.WriteLine("\n1. Delayed");
                    Console.WriteLine("2. Boarding");
                    Console.WriteLine("3. On Time");

                    // ✅ Step 7: Loop until the user selects a valid status
                    while (true)
                    {
                        Console.Write("Please select the new status of the flight: ");
                        string? statusChoice = Console.ReadLine()?.Trim();

                        if (statusChoice == "1")
                        {
                            flight.Status = "Delayed";
                            break;
                        }
                        else if (statusChoice == "2")
                        {
                            flight.Status = "Boarding";
                            break;
                        }
                        else if (statusChoice == "3")
                        {
                            flight.Status = "On Time";
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please try again.");
                        }
                    }
                    statusUpdated = true;
                }
                else if (updateStatus == "N")
                {
                    flight.Status = "On Time";
                    statusUpdated = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter Y or N.");
                }
            }

            Console.WriteLine($"\nFlight {flightNum} has been assigned to Boarding Gate {gateName}!");
            Console.WriteLine("\n=============================================\n");
        }




        void CreateNewFlight(Terminal terminal)
        {
            bool run = true;
            bool run2 = true;
            while (run)
            {
                Console.Write("Enter Flight Number: ");
                string? flightNum = Console.ReadLine();
                Console.Write("Enter Origin: ");
                string? origin = Console.ReadLine();
                Console.Write("Enter Destination: ");
                string? destination = Console.ReadLine();
                DateTime expectedTime;
                while (true)
                {
                    try
                    {
                        Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
                        expectedTime = Convert.ToDateTime(Console.ReadLine());
                        break;
                    }
                    catch (FormatException ex) 
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                while (run2)
                {
                    Console.WriteLine("Would you like to enter any additional information?(Y/N) ");
                    string? ans = Console.ReadLine();
                    if (ans == "Y")
                    {
                        while (true)
                        {
                            Console.Write("Enter Special Request Code: ");
                            string? code = Console.ReadLine();
                            if (code == "CFFT")
                            {
                                Flight flight = new CFFTFlight(flightNum, origin, destination, expectedTime);
                                terminal.AddFlight(flight);
                                run2 = false;
                                break;
                            }
                            else if (code == "DDJB")
                            {
                                Flight flight = new DDJBFlight(flightNum, origin, destination, expectedTime);
                                terminal.AddFlight(flight);
                                run2 = false;
                                break;
                            }
                            else if (code == "LWTT")
                            {
                                Flight flight = new LWTTFlight(flightNum, origin, destination, expectedTime);
                                terminal.AddFlight(flight);
                                run2 = false;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please try again.");
                            }
                        }
                    }
                    else if (ans == "N")
                    {
                        Flight flight1 = new NORMFlight(flightNum, origin, destination, expectedTime);
                        terminal.AddFlight(flight1);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                    }
                }
                using (StreamWriter sw = new StreamWriter("flights.csv", true))
                {
                    sw.WriteLine($"{flightNum},{origin},{destination},{expectedTime}");
                }
                while (true)
                {
                    Console.Write("Would you like to add another flight?(Y/N) ");
                    if (Console.ReadLine() == "Y")
                    {
                        break;
                    }
                    else if (Console.ReadLine() == "N")
                    {
                        run = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                    }
                }
            }
            Console.WriteLine("Flight(s) have been successfully added.");
        }

        //Display Flight Schedule in Chronological Order (Matches Sample Output)
        static void DisplayFlightSchedule(Terminal terminal)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");

            // ✅ Step 1: Check if there are no flights
            if (terminal.Flights.Count == 0)
            {
                Console.WriteLine("No flights available.");
                return; // Exit the function if there are no flights
            }

            // ✅ Step 2: Convert the Flights Dictionary to a List and sort it by ExpectedTime
            List<Flight> flights = new List<Flight>(terminal.Flights.Values);
            flights.Sort(); // Uses the CompareTo() method in Flight.cs to sort by ExpectedTime

            // ✅ Step 3: Print the column headers in the required format
            Console.WriteLine("{0,-15} {1,-25} {2,-25} {3,-25} {4,-35} {5,-15} {6,-15}",
                              "Flight Number", "Airline Name", "Origin", "Destination",
                              "Expected Departure/Arrival Time", "Status", "Boarding Gate");

            // ✅ Step 4: Loop through each flight and print details
            foreach (Flight flight in flights)
            {
                // Retrieve the airline associated with the flight
                Airline airline = terminal.GetAirlineFromFlight(flight);

                // If no airline is found, assign a placeholder name
                string airlineName = airline != null ? airline.Name : "Unknown Airline";

                // If no boarding gate is assigned, display "Unassigned"
                string boardingGate = "Unassigned";
                foreach (var gate in terminal.BoardingGates.Values)
                {
                    if (gate.AssignedFlight == flight)
                    {
                        boardingGate = gate.GateName;
                        break;
                    }
                }

                // Print flight details in the correct format
                Console.WriteLine("{0,-15} {1,-25} {2,-25} {3,-25} {4,-35} {5,-15} {6,-15}",
                                  flight.FlightNumber, airlineName, flight.Origin, flight.Destination,
                                  flight.ExpectedTime.ToString("dd/M/yyyy hh:mm:ss tt"), flight.Status, boardingGate);
            }

            Console.WriteLine("=============================================");
        }

    }
}
