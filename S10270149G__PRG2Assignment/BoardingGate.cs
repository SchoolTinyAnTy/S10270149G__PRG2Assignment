using S10270149G__PRG2Assignment;

public class BoardingGate
{
    public string GateName { get; private set; } // The name of the gate (e.g., A1, B2)
    public bool SupportsCFFT { get; private set; } // Whether gate supports CFFT request
    public bool SupportsDDJB { get; private set; } // Whether gate supports DDJB request
    public bool SupportsLWTT { get; private set; } // Whether gate supports LWTT request
    public Flight AssignedFlight { get; set; } // Flight assigned to this gate

    // Constructor to initialize boarding gate properties
    public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT)
    {
        GateName = gateName;
        SupportsCFFT = supportsCFFT;
        SupportsDDJB = supportsDDJB;
        SupportsLWTT = supportsLWTT;
        AssignedFlight = null; // Initially no flight assigned
    }

    // Calculate fees for the gate based on assigned flight special request
    public double CalculateFees()
    {
        double baseFee = 300; // Default base fee for all gates
        if (AssignedFlight != null) // If a flight is assigned, calculate additional fees
        {
            if (AssignedFlight.SpecialRequest == "DDJB") baseFee += 300;
            if (AssignedFlight.SpecialRequest == "CFFT") baseFee += 150;
            if (AssignedFlight.SpecialRequest == "LWTT") baseFee += 500;
        }
        return baseFee;
    }

    // Override ToString() to return details of the gate
    public override string ToString()
    {
        string assignedFlightInfo = AssignedFlight != null ? AssignedFlight.FlightNumber : "None";
        return $"Gate: {GateName}, Supports CFFT: {SupportsCFFT}, DDJB: {SupportsDDJB}, LWTT: {SupportsLWTT}, Assigned Flight: {assignedFlightInfo}";
    }
}
