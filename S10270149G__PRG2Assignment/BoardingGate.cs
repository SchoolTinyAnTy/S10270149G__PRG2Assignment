using S10270149G__PRG2Assignment;  // Namespace for the assignment

class BoardingGate
{
    public string GateName { get; set; }  // The name of the gate (e.g., A1, B2)
    public bool SupportsCFFT { get; set; }  // Whether the gate supports CFFT requests
    public bool SupportsDDJB { get; set; }  // Whether the gate supports DDJB requests
    public bool SupportsLWTT { get; set; }  // Whether the gate supports LWTT requests
    public Flight? AssignedFlight { get; set; }  // The flight currently assigned to the gate

    // Constructor to initialize boarding gate properties
    public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT)
    {
        GateName = gateName;  // Assign gate name
        SupportsCFFT = supportsCFFT;  // Assign whether CFFT is supported
        SupportsDDJB = supportsDDJB;  // Assign whether DDJB is supported
        SupportsLWTT = supportsLWTT;  // Assign whether LWTT is supported
        AssignedFlight = null;  // No flight assigned initially
    }

    // Calculates fees based on the assigned flight's special request
    public double CalculateFees()
    {
        double baseFee = 300;  // Base fee for using the gate
        if (AssignedFlight != null)  // Check if a flight is assigned
        {
            // Add extra fees based on the flight's special request
            if (AssignedFlight is DDJBFlight) baseFee += 300;
            if (AssignedFlight is CFFTFlight) baseFee += 150;
            if (AssignedFlight is LWTTFlight) baseFee += 500;
        }
        return baseFee;  // Return the calculated fee
    }

    // Converts boarding gate information to a string
    public override string ToString()
    {
        string assignedFlightInfo = AssignedFlight != null ? AssignedFlight.FlightNumber : "None";
        return $"Gate: {GateName}, Supports CFFT: {SupportsCFFT}, DDJB: {SupportsDDJB}, LWTT: {SupportsLWTT}, Assigned Flight: {assignedFlightInfo}";
    }
}
