//==========================================================
// Student Number : S10270149G
// Partner Number : S10266900J
// Student Name   : Orlando Lee Ming Kai
// Partner Name   : Ewe Yoke Kay 
//==========================================================
namespace S10270149G__PRG2Assignment;

// Class representing a boarding gate
public class BoardingGate
{
    public string GateName { get; private set; }  // Gate identifier (e.g., "A1")
    public bool SupportsCFFT { get; private set; }  // Whether the gate supports CFFT requests
    public bool SupportsDDJB { get; private set; }  // Whether the gate supports DDJB requests
    public bool SupportsLWTT { get; private set; }  // Whether the gate supports LWTT requests
    public Flight? AssignedFlight { get; private set; }  // Flight assigned to the gate

    // Constructor to initialize boarding gate properties
    public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT)
    {
        GateName = gateName;
        SupportsCFFT = supportsCFFT;
        SupportsDDJB = supportsDDJB;
        SupportsLWTT = supportsLWTT;
        AssignedFlight = null;  // Initially no flight assigned
    }

    // Calculates fees based on the assigned flight's special request
    public double CalculateFees()
    {
        double baseFee = 300;  // Base fee for gate usage
        if (AssignedFlight is DDJBFlight) baseFee += 300;
        if (AssignedFlight is CFFTFlight) baseFee += 150;
        if (AssignedFlight is LWTTFlight) baseFee += 500;
        return baseFee;
    }

    // Returns gate details as a formatted string
    public override string ToString()
    {
        string assignedFlightInfo = AssignedFlight != null ? AssignedFlight.FlightNumber : "None";
        return $"Gate: {GateName}, Assigned Flight: {assignedFlightInfo}";
    }
}
