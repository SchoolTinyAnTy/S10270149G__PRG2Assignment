//==========================================================
// Student Number : S10270149G
// Partner Number : S10266900J
// Student Name   : Orlando Lee Ming Kai
// Partner Name   : Ewe Yoke Kay 
//==========================================================
using S10270149G__PRG2Assignment;
using System;
using System.ComponentModel.Design;
using System.Diagnostics.Tracing;
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
            Console.WriteLine("8. Bulk Process Unassigned Flights");
            Console.WriteLine("9. Display Total Fee Per Airline");
            Console.WriteLine("0. Exit");

            Console.Write("\nPlease select your option: ");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "0":
                    Console.WriteLine("Goodbye!");
                    return;
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
                case "5":
                    DisplayAirlineFlights(terminal);
                    break;
                case "6":
                    ModifyFlightDetails(terminal);
                    break;
                case "7":
                    DisplayFlightSchedule(terminal);
                    break;
                case "8":
                    ProcessUnassignedFlights(terminal);
                    break;
                case "9":
                    DisplayTotalFeePerAirline(terminal);
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

        // ✅ Feature 3 (Option 1): List all flights with their basic information
        void ListAllFlights(Terminal terminal)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Flights for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-22}{"Origin",-22}{"Destination",-22}Expected Departure/Arrival Time");
            foreach (Flight flight in terminal.Flights.Values)
            {
                Console.WriteLine($"{flight.FlightNumber,-15}{terminal.GetAirlineFromFlight(flight).Name,-22}{flight.Origin,-22}{flight.Destination,-22}{flight.ExpectedTime.ToString("dd/M/yyyy hh:mm tt")}");
            }
        }

        // ✅ Feature 5: Assign a Boarding Gate to a Flight (Matches Sample Output)
        void AssignBoardingGate(Terminal terminal)
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

        //✅ Feature 6 (Option 4): Create a new flight
        void CreateNewFlight(Terminal terminal)
        {
            bool run = true;
            bool run2 = true;
            bool run3 = true;
            while (run)
            {
                string? flightNum;
                while (true)
                {
                    try
                    {
                        Console.Write("Enter Flight Number: ");
                        flightNum = Console.ReadLine()?.ToUpper();
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                    }
                }
                string? origin;
                while (true)
                {
                    try
                    {
                        Console.Write("Enter Origin: ");
                        origin = Console.ReadLine();
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                    }
                }
                string? destination;
                while (true)
                {
                    try
                    {
                        Console.Write("Enter Destination: ");
                        destination = Console.ReadLine();
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                    }
                }
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

        //✅ Feature 9 (Option 7): Display scheduled flights in chronological order
        void DisplayFlightSchedule(Terminal terminal)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            List<Flight> flights = new List<Flight>();
            Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-22}{"Origin",-22}{"Destination",-22}{"Expected Departure/Arrival Time",-35}{"Status",-15}Boarding Gate");
            foreach (Flight flight in terminal.Flights.Values)
            {
                flights.Add(terminal.Flights[flight.FlightNumber]);
            }
            flights.Sort();
            foreach (Flight flight in terminal.Flights.Values)
            {
                BoardingGate boardingGate = null;
                foreach (BoardingGate boardinggate in terminal.BoardingGates.Values)
                {
                    if (boardinggate.AssignedFlight == flight)
                    {
                        boardingGate = boardinggate;
                    }
                }
                Console.WriteLine($"{flight.FlightNumber,-15}{terminal.GetAirlineFromFlight(flight).Name,-22}{flight.Origin,-22}{flight.Destination,-22}{flight.ExpectedTime.ToString("dd/M/yyyy hh:mm tt"),-35}{flight.Status,-15}{boardingGate.GateName}");
            }
        }
        // ✅ Feature 7 (Option 5): Display Flight Schedule per Airline (Sorted by Flight Number)
        void DisplayAirlineFlights(Terminal terminal)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");

            // ✅ Step 1: Check if there are no airlines
            if (terminal.Airlines.Count == 0)
            {
                Console.WriteLine("No airlines available.");
                return; // Exit the function if there are no airlines
            }

            // ✅ Step 2: Display list of airlines
            Console.WriteLine("{0,-15} {1,-30}", "Airline Code", "Airline Name");
            foreach (var airline in terminal.Airlines.Values)
            {
                Console.WriteLine("{0,-15} {1,-30}", airline.Code, airline.Name);
            }

            // ✅ Step 3: Prompt the user to select an airline code
            Console.Write("\nEnter Airline Code: ");
            string airlineCode = Console.ReadLine()?.Trim().ToUpper();

            // ✅ Step 4: Validate airline input
            if (!terminal.Airlines.ContainsKey(airlineCode))
            {
                Console.WriteLine("Error: Airline not found. Please enter a valid airline code.");
                return;
            }

            Airline selectedAirline = terminal.Airlines[airlineCode];

            // ✅ Step 5: Get all flights for the selected airline
            List<Flight> airlineFlights = new List<Flight>();
            foreach (var flight in terminal.Flights.Values)
            {
                if (terminal.GetAirlineFromFlight(flight).Code == airlineCode)
                {
                    airlineFlights.Add(flight);
                }
            }

            // ✅ Step 6: Check if the airline has no flights
            if (airlineFlights.Count == 0)
            {
                Console.WriteLine($"No flights found for {selectedAirline.Name}.");
                return;
            }

            // ✅ Step 7: Sort flights by Flight Number in ascending order
            airlineFlights.Sort((flight1, flight2) =>
                int.Parse(flight1.FlightNumber.Split(' ')[1]).CompareTo(int.Parse(flight2.FlightNumber.Split(' ')[1]))
            );

            // ✅ Step 8: Print header for flights under selected airline
            Console.WriteLine("\n=============================================");
            Console.WriteLine($"List of Flights for {selectedAirline.Name}");
            Console.WriteLine("=============================================");
            Console.WriteLine("{0,-15} {1,-25} {2,-25} {3,-35}",
                              "Flight Number", "Airline Name", "Origin",
                              "Expected Departure/Arrival Time");

            // ✅ Step 9: Print flight details
            foreach (var flight in airlineFlights)
            {
                Console.WriteLine("{0,-15} {1,-25} {2,-25} {3,-35}",
                                  flight.FlightNumber, selectedAirline.Name, flight.Origin,
                                  flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt"));
            }

            Console.WriteLine("=============================================");
        }


        // ✅ Feature 8 (Option 6): Modify Flight Details (Matches Sample Output)
        void ModifyFlightDetails(Terminal terminal)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");

            // ✅ Step 1: Check if there are no airlines
            if (terminal.Airlines.Count == 0)
            {
                Console.WriteLine("No airlines available.");
                return; // Exit the function if there are no airlines
            }

            // ✅ Step 2: Display list of airlines
            Console.WriteLine("{0,-15} {1,-30}", "Airline Code", "Airline Name");
            foreach (var airline in terminal.Airlines.Values)
            {
                Console.WriteLine("{0,-15} {1,-30}", airline.Code, airline.Name);
            }

            // ✅ Step 3: Prompt the user to select an airline code
            Console.Write("\nEnter Airline Code: ");
            string airlineCode = Console.ReadLine()?.Trim().ToUpper();

            // ✅ Step 4: Validate airline input
            if (!terminal.Airlines.ContainsKey(airlineCode))
            {
                Console.WriteLine("Error: Airline not found. Please enter a valid airline code.");
                return;
            }

            Airline selectedAirline = terminal.Airlines[airlineCode];

            // ✅ Step 5: Get all flights for the selected airline
            List<Flight> airlineFlights = terminal.Flights.Values.Where(f => terminal.GetAirlineFromFlight(f).Code == airlineCode).ToList();

            // ✅ Step 6: Check if the airline has no flights
            if (airlineFlights.Count == 0)
            {
                Console.WriteLine($"No flights found for {selectedAirline.Name}.");
                return;
            }

            // ✅ Step 7: Sort flights by Flight Number in ascending order
            airlineFlights.Sort((flight1, flight2) =>
                int.Parse(flight1.FlightNumber.Split(' ')[1]).CompareTo(int.Parse(flight2.FlightNumber.Split(' ')[1]))
            );

            // ✅ Step 8: Print flight details
            Console.WriteLine("\nList of Flights for " + selectedAirline.Name);
            Console.WriteLine("{0,-15} {1,-25} {2,-25} {3,-35}",
                              "Flight Number", "Airline Name", "Origin",
                              "Expected Departure/Arrival Time");

            foreach (var flight in airlineFlights)
            {
                Console.WriteLine("{0,-15} {1,-25} {2,-25} {3,-35}",
                                  flight.FlightNumber, selectedAirline.Name, flight.Origin,
                                  flight.ExpectedTime.ToString("dd/MM/yyyy HH:mm"));
            }

            // ✅ Step 9: Ask the user which flight to modify
            Console.Write("\nChoose an existing Flight to modify or delete: ");
            string flightNum = Console.ReadLine()?.ToUpper();

            // ✅ Step 10: Validate flight input
            if (!terminal.Flights.ContainsKey(flightNum))
            {
                Console.WriteLine("Error: Flight number not found. Please enter a valid flight number.");
                return;
            }

            Flight selectedFlight = terminal.Flights[flightNum];

            // ✅ Step 11: Ask if the user wants to modify or delete the flight
            Console.WriteLine("\n1. Modify Flight");
            Console.WriteLine("2. Delete Flight");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine()?.Trim();

            if (choice == "2")
            {
                terminal.Flights.Remove(flightNum);
                Console.WriteLine($"Flight {flightNum} has been successfully deleted.");
                return;
            }

            // ✅ Step 12: Ask what the user wants to modify
            Console.WriteLine("\n1. Modify Basic Information");
            Console.WriteLine("2. Modify Status");
            Console.WriteLine("3. Modify Special Request Code");
            Console.WriteLine("4. Modify Boarding Gate");
            Console.Write("Choose an option: ");
            string modifyChoice = Console.ReadLine()?.Trim();

            switch (modifyChoice)
            {
                case "1":
                    // ✅ Modify Basic Information
                    Console.Write("Enter new Origin: ");
                    selectedFlight.Origin = Console.ReadLine()?.Trim();
                    Console.Write("Enter new Destination: ");
                    selectedFlight.Destination = Console.ReadLine()?.Trim();
                    // ✅ Fixing Date Format Issue
                    Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
                    DateTime newTime;
                    while (!DateTime.TryParseExact(Console.ReadLine(),
                        new[] { "dd/MM/yyyy HH:mm", "d/M/yyyy HH:mm", "dd/M/yyyy HH:mm" }, // ✅ Supports different variations
                        null,
                        System.Globalization.DateTimeStyles.None,
                        out newTime))
                    {
                        Console.WriteLine("Error: Invalid date format. Please use dd/MM/yyyy HH:mm.");
                        Console.Write("Enter new Expected Departure/Arrival Time: ");
                    }
                    selectedFlight.ExpectedTime = newTime;
                    Console.WriteLine("Flight updated!");
                    break;


                case "2":
                    // ✅ Modify Flight Status
                    Console.WriteLine("\n1. Delayed");
                    Console.WriteLine("2. Boarding");
                    Console.WriteLine("3. On Time");
                    Console.Write("Please select the new status of the flight: ");
                    selectedFlight.Status = Console.ReadLine()?.Trim().ToUpper() switch
                    {
                        "1" => "Delayed",
                        "2" => "Boarding",
                        "3" => "On Time",
                        _ => selectedFlight.Status
                    };
                    Console.WriteLine("Flight status updated!");
                    break;

                case "3":
                    // ✅ Modify Special Request Code
                    Console.Write("\nEnter new Special Request Code (None, CFFT, DDJB, LWTT): ");
                    string requestCode = Console.ReadLine().ToUpper();
                    if (requestCode == "CFFT")
                        selectedFlight = new CFFTFlight(selectedFlight.FlightNumber, selectedFlight.Origin, selectedFlight.Destination, selectedFlight.ExpectedTime);
                    else if (requestCode == "DDJB")
                        selectedFlight = new DDJBFlight(selectedFlight.FlightNumber, selectedFlight.Origin, selectedFlight.Destination, selectedFlight.ExpectedTime);
                    else if (requestCode == "LWTT")
                        selectedFlight = new LWTTFlight(selectedFlight.FlightNumber, selectedFlight.Origin, selectedFlight.Destination, selectedFlight.ExpectedTime);
                    else
                        selectedFlight = new NORMFlight(selectedFlight.FlightNumber, selectedFlight.Origin, selectedFlight.Destination, selectedFlight.ExpectedTime);
                    terminal.Flights[flightNum] = selectedFlight;
                    Console.WriteLine("\nSpecial Request Code updated!");
                    break;

                case "4":
                    // ✅ Modify Boarding Gate
                    Console.Write("\nEnter new Boarding Gate: ");
                    string gateName = Console.ReadLine().ToUpper();

                    if (!terminal.BoardingGates.ContainsKey(gateName))
                    {
                        Console.WriteLine("Error: Boarding Gate not found.");
                        return;
                    }

                    BoardingGate gate = terminal.BoardingGates[gateName];
                    gate.AssignedFlight = selectedFlight;
                    Console.WriteLine("\nBoarding Gate updated!");
                    break;

                default:
                    Console.WriteLine("Error: Invalid selection.");
                    return;
            }

            // ✅ Step 13: Determine Special Request Code
            string specialRequestCode = selectedFlight switch
            {
                CFFTFlight => "CFFT",
                DDJBFlight => "DDJB",
                LWTTFlight => "LWTT",
                _ => "None"
            };

            // ✅ Step 14: Determine Boarding Gate Assignment
            string boardingGate = terminal.BoardingGates.Values.FirstOrDefault(g => g.AssignedFlight == selectedFlight)?.GateName ?? "Unassigned";

            // ✅ Step 15: Print updated flight details
            Console.WriteLine($"Flight Number: {selectedFlight.FlightNumber}");
            Console.WriteLine($"Airline Name: {selectedAirline.Name}");
            Console.WriteLine($"Origin: {selectedFlight.Origin}");
            Console.WriteLine($"Destination: {selectedFlight.Destination}");
            Console.WriteLine($"Expected Departure/Arrival Time: {selectedFlight.ExpectedTime:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Status: {selectedFlight.Status}");
            Console.WriteLine($"Special Request Code: {(selectedFlight is NORMFlight ? "None" : selectedFlight.GetType().Name.Replace("Flight", "").ToUpper())}");
            Console.WriteLine($"Boarding Gate: {terminal.BoardingGates.Values.FirstOrDefault(g => g.AssignedFlight == selectedFlight)?.GateName ?? "Unassigned"}");
            Console.WriteLine("=============================================");
        }


        // ✅ Advanced Feature (a): Process Unassigned Flights in Bulk
        void ProcessUnassignedFlights(Terminal terminal)
        {
            Console.WriteLine("\n=============================================");
            Console.WriteLine("Processing Unassigned Flights to Boarding Gates");
            Console.WriteLine("=============================================");

            Queue<Flight> unassignedFlightsQueue = new Queue<Flight>();

            // ✅ Step 1: Identify flights without assigned boarding gates
            foreach (var flight in terminal.Flights.Values)
            {
                if (!terminal.BoardingGates.Values.Any(g => g.AssignedFlight == flight))
                {
                    unassignedFlightsQueue.Enqueue(flight);
                }
            }

            int totalUnassignedFlights = unassignedFlightsQueue.Count;
            Console.WriteLine($"Total Unassigned Flights: {totalUnassignedFlights}");

            // ✅ Step 2: Identify unassigned boarding gates
            int totalUnassignedGates = terminal.BoardingGates.Values.Count(g => g.AssignedFlight == null);
            Console.WriteLine($"Total Unassigned Boarding Gates: {totalUnassignedGates}");

            int flightsProcessed = 0;
            int gatesProcessed = 0;

            // ✅ Step 3: Process each flight in the queue
            while (unassignedFlightsQueue.Count > 0)
            {
                Flight currentFlight = unassignedFlightsQueue.Dequeue();
                string specialRequest = currentFlight is CFFTFlight ? "CFFT" :
                                        currentFlight is DDJBFlight ? "DDJB" :
                                        currentFlight is LWTTFlight ? "LWTT" : "None";

                BoardingGate assignedGate = null;

                // ✅ Step 4: Find a suitable boarding gate
                if (specialRequest != "None")
                {
                    assignedGate = terminal.BoardingGates.Values.FirstOrDefault(g =>
                        g.AssignedFlight == null &&
                        ((specialRequest == "CFFT" && g.SupportsCFFT) ||
                         (specialRequest == "DDJB" && g.SupportsDDJB) ||
                         (specialRequest == "LWTT" && g.SupportsLWTT)));
                }

                if (assignedGate == null)
                {
                    // ✅ If no special request or no matching gate found, assign any unassigned gate
                    assignedGate = terminal.BoardingGates.Values.FirstOrDefault(g => g.AssignedFlight == null);
                }

                if (assignedGate != null)
                {
                    assignedGate.AssignedFlight = currentFlight;
                    flightsProcessed++;
                    gatesProcessed++;

                    // ✅ Display flight details after processing
                    Console.WriteLine("\n=============================================");
                    Console.WriteLine("Updated Flight Details:");
                    Console.WriteLine($"Flight Number: {currentFlight.FlightNumber}");
                    Console.WriteLine($"Airline Name: {terminal.GetAirlineFromFlight(currentFlight).Name}");
                    Console.WriteLine($"Origin: {currentFlight.Origin}");
                    Console.WriteLine($"Destination: {currentFlight.Destination}");
                    Console.WriteLine($"Expected Departure/Arrival Time: {currentFlight.ExpectedTime:d/M/yyyy h:mm:ss tt}");
                    Console.WriteLine($"Special Request Code: {specialRequest}");
                    Console.WriteLine($"Assigned Boarding Gate: {assignedGate.GateName}");
                    Console.WriteLine("=============================================");
                }
            }

            // ✅ Step 5: Display final summary
            Console.WriteLine("\n=============================================");
            Console.WriteLine("Bulk Processing Summary");
            Console.WriteLine("=============================================");
            Console.WriteLine($"Total Flights Processed: {flightsProcessed}");
            Console.WriteLine($"Total Boarding Gates Assigned: {gatesProcessed}");

            int totalFlights = terminal.Flights.Count;
            int percentageAssigned = (int)((flightsProcessed / (double)totalFlights) * 100);
            Console.WriteLine($"Percentage of Flights Assigned Automatically: {percentageAssigned}%");
            Console.WriteLine("=============================================");
        }

        // ✅ Advanced Feature (b): Compute Total Fee Per Airline for the Day
        void DisplayTotalFeePerAirline(Terminal terminal)
        {
            Console.WriteLine("\n=============================================");
            Console.WriteLine("Total Fees Per Airline for the Day");
            Console.WriteLine("=============================================");

            // ✅ Step 1: Check if all flights have assigned boarding gates
            List<Flight> unassignedFlights = terminal.Flights.Values.Where(f => !terminal.BoardingGates.Values.Any(g => g.AssignedFlight == f)).ToList();

            if (unassignedFlights.Count > 0)
            {
                Console.WriteLine("Warning: Some flights do not have assigned boarding gates.");
                Console.WriteLine("Please ensure all flights are assigned a gate before running this feature again.");
                return;
            }

            double totalAirlineFees = 0;
            double totalDiscounts = 0;
            Dictionary<string, double> airlineFees = new Dictionary<string, double>();
            Dictionary<string, double> airlineDiscounts = new Dictionary<string, double>();
            Console.WriteLine($"{"Airline Name",-20}{"Original subtotal",-20}{"Subtotal of discounts"}");
            // ✅ Step 2: Calculate fees per airline
            foreach (var airline in terminal.Airlines.Values)
            {
                double airlineTotalFee = 0;
                double airlineDiscount = 0;

                List<Flight> airlineFlights = terminal.Flights.Values.Where(f => terminal.GetAirlineFromFlight(f) == airline).ToList();
                double totalFlights = airlineFlights.Count;

                // ✅ Step 3: Calculate fees per flight
                foreach (var flight in airlineFlights)
                {
                    double flightFee = 0;

                    // ✅ Apply Singapore-related fees and special request fees
                    flightFee += flight.CalculateFees();

                    // ✅ Apply boarding gate base fee
                    flightFee += 300;

                    airlineTotalFee += flightFee;
                }

                // ✅ Apply promotional discounts 
                airlineDiscount += Math.Floor(totalFlights / 3) * 350;
                foreach (var flight in airlineFlights)
                {
                    if (flight.ExpectedTime.Hour < 11 && flight.ExpectedTime.Hour > 21)
                    {
                        airlineDiscount += 110;
                    }
                    if (flight.Origin == "Dubai (DXB)")
                    {
                        airlineDiscount += 25;
                    }
                    if (flight.Origin == "Bangkok (BKK)")
                    {
                        airlineDiscount += 25;
                    }
                    if (flight.Origin == "Tokyo (NRT)")
                    {
                        airlineDiscount += 25;
                    }

                    if (flight is NORMFlight)
                    {
                        airlineDiscount += 50;
                    }

                    if (airlineFlights.Count > 5)
                    {
                        airlineDiscount = airlineTotalFee * 0.03;
                    }
                }


                // ✅ Store calculations for final report
                airlineFees[airline.Code] = airlineTotalFee;
                airlineDiscounts[airline.Code] = airlineDiscount;
                totalAirlineFees += airlineTotalFee;
                totalDiscounts += airlineDiscount;

                Console.WriteLine($"{airline.Name,-20}${airlineTotalFee,-20}${airlineDiscount}");
            }
            Console.WriteLine($"\n{"Total Subtotal",-20}{"Total Discounts",-20}{"Final Total Fees",-20}{"Discount Percentage"}");
            double finalTotalFees = totalAirlineFees - totalDiscounts;
            double discountPercentage = (totalDiscounts / totalAirlineFees) * 100;
            Console.WriteLine($"${totalAirlineFees,-19}${totalDiscounts,-19}${finalTotalFees,-19}{discountPercentage}%");
        }
    }
}
