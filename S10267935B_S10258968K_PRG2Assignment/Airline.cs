using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267935B_S10258968K_PRG2Assignment
{
    class Airline
    {
        //Properties
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights{ get; set; }
        //Methods
        public bool AddFlight(Flight f)
        {

        }
        public double CalculateFees()
        {
            
        }
        public bool RemoveFlight(Flight f)
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
        //Constructors
        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
