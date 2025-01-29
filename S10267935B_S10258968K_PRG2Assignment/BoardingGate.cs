using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number	: S10267935B, S10258968K
// Student Name	: Dylan Wong
// Partner Name	: Ying Zhi
//==========================================================
namespace S10267935B_S10258968K_PRG2Assignment
{
    class BoardingGate
    {
        //Properties
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight? Flight { get; set; }
        //Methods
        public double CalculateFees()
        {
            if (Flight == null)
                return 0;
            double totalFees = 300; 
            //if (Flight.Destination.Contains("SIN")) 
            //    totalFees += 500;

            //if (Flight.Origin.Contains("SIN"))  
            //    totalFees += 800;

            //switch (Flight.Status)  
            //{
            //    case "DDJB":
            //        totalFees += 300;
            //        break;
            //    case "CFFT":
            //        totalFees += 150;
            //        break;
            //    case "LWTT":
            //        totalFees += 500;
            //        break;
            //}
            return totalFees;
        }
        public override string ToString()
        {
            return $"Gate Name: {GateName,-10} Supports CFFT: {SupportsCFFT,-10} Supports DDJB: {SupportsDDJB,-10} Supports LWTT: {SupportsLWTT,-10}";
        }
        //Constructor
        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT,Flight? flight = null)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
            Flight = flight;
        }
    }
}
