using S10270149G__PRG2Assignment;
using System;

public class BoardingGate
{
    // Attributes
    public string GateName { get; private set; }
    public bool SupportsCFFT { get; private set; }
    public bool SupportsDDJB { get; private set; }
    public bool SupportsLWTT { get; private set; }
    public Flight AssignedFlight { get; set; }

    // Constructor
    public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT)
    {
        GateName = gateName;
        SupportsCFFT = supportsCFFT;
        SupportsDDJB = supportsDDJB;    
        SupportsLWTT = supportsLWTT;
        AssignedFlight = null;
    }

    // Method to calculate fees
    public double CalculateFees()
    {
        double baseFee = 300;
        if (AssignedFlight != null)
        {
            if (AssignedFlight.SpecialRequest == "DDJB") baseFee += 300;
            if (AssignedFlight.SpecialRequest == "CFFT") baseFee += 150;
            if (AssignedFlight.SpecialRequest == "LWTT") baseFee += 500;
        }
        return baseFee;
    }

    // Overriding ToString method
    public override string ToString()
    {
        string assignedFlightInfo = AssignedFlight != null ? AssignedFlight.FlightNumber : "None";
        return $"Gate Name: {GateName}, Supports CFFT: {SupportsCFFT}, Supports DDJB: {SupportsDDJB}, Supports LWTT: {SupportsLWTT}, Assigned Flight: {assignedFlightInfo}";
    }
}
