using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10270149G__PRG2Assignment
{
    public class Airline
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public Airline(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }

}
