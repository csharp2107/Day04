using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySerialization.Create();
            //XMLSerialization.Create();
            //SoapSerialization.Create();
            //JsonSerialization1.Create();
            //JsonSerialization2.Create();
            //JsonSerialization3.Create();
            //JsonSerialization3.ErrorHandle();
            //JsonSerialization3.MissingMembers();
            //JsonSerialization3.NBP();
            PerformanceCheck.Run();
        }
    }
}
