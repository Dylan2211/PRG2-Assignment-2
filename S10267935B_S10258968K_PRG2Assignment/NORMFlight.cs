﻿using System;
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
    class NORMFlight : Flight
    {
        // Methods
        public override double CalculateFees()
        {
            // First call the base method to get the initial fee
            double fee = base.CalculateFees();
            // No Special Request Code
            fee -= 50;
            return fee;
        }
        public override string ToString()
        {
            return base.ToString();
        }
        // Constructor
        public NORMFlight(string flightNumber, string flightOrigin, string flightDestination, DateTime flightExpectedTime, string flightStatus) : base(flightNumber, flightOrigin, flightDestination, flightExpectedTime, flightStatus)
        {
        }
    }
}